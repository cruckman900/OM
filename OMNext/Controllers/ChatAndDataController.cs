using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using OMNext.Data;
using OMNext.Models;
using OMNext.Helpers;
using System.Diagnostics;
using System.Security.Cryptography;

namespace OMNext.Controllers
{
    public class ChatAndDataController : Controller
    {
        private readonly OM2018Context _context;
        private readonly IWebHostEnvironment _env;
        protected Helper _helper = new Helper();

        /// <summary>
        /// Inject the database context
        /// </summary>
        /// <param name="context">The context to be used</param>
        public ChatAndDataController(OM2018Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost]
        public async Task<JsonResult> InsertChatMessage(Chat oChat)
        {
            Helper h = new Helper();
            Aes aes = Aes.Create();

            oChat.SentDateTime = DateTime.Now;

            _context.Chats.Add(oChat);
            await _context.SaveChangesAsync();
            return Json(oChat);
        }

        [HttpGet]
        public async Task<bool> AllowsMedComm(string clcCenter)
        {
            try
            {
                CLCCenter c = await _context.CLCCenters.Where(c => c.Abbreviation == clcCenter).FirstOrDefaultAsync();

                if (c == null)
                    return false;

                if ((bool)c.HasAccessToMedComm)
                    return true;

                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + ": " + e.StackTrace);
            }
            return false;
        }

        [HttpGet]
        public async Task<bool> HasMedComTeam(string booth)
        {
            try
            {
                RunningMission rm = await _context.RunningMissions
                    .OrderByDescending(x => x.MissionID)
                    .FirstOrDefaultAsync(x => x.Booth == booth);

                if (rm == null)
                    return false;

                Script script = await _context.Scripts.FirstOrDefaultAsync(x => x.ScriptID == rm.ScriptID);

                string title = script.Title.ToUpper();
                if (title.IndexOf("MED", StringComparison.CurrentCulture) >= 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + ": " + e.StackTrace);
            }
            return false;
        }

        [HttpGet]
        public TeamName GetTeam(int teamID)
        {
            try
            {
                TeamName teamName = (TeamName)teamID;
                return teamName;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + ": " + e.StackTrace);
            }
            return new TeamName();
        }

        [HttpGet]
        public async Task<int> GetTeamID(int pMissionID, int pTeamID)
        {
            try
            {
                Team t = await _context.Teams.FirstOrDefaultAsync(x => x.MissionID == pMissionID && x.TeamName == (TeamName)pTeamID);
                if (t != null)
                    return t.TeamID;
                else
                    return -1;
            } catch (Exception)
            {
                return 0;
            }

            //return pTeamID;
        }

        [HttpGet]
        public async Task<DataDrive> GetDataDrive(int pMissionID, int pTeamID)
        {
            try
            {
                DataDrive oDataDrive = await _context.DataDrives.SingleOrDefaultAsync(x => x.MissionID == pMissionID && x.TeamID == pTeamID);

                return oDataDrive;
            }
            catch (Exception e)
            {
                Debug.Write(e.Message + ": " + e.StackTrace);
            }
            return new DataDrive();
        }

        [HttpGet]
        public async Task<VolcanoData> GetCurrentGMT(int pMissionID)
        {
            try
            {
                RunningMission runningMission = await _context.RunningMissions
                    .SingleOrDefaultAsync(x => x.MissionID == pMissionID);

                MissionEnding missionEnding = runningMission.MissionEnding;

                Team theTeam = await _context.Teams.SingleOrDefaultAsync(x => x.MissionID == pMissionID && x.TeamName == (TeamName)2);

                int teamID = 0;
                if (theTeam != null)
                {
                    teamID = theTeam.TeamID;
                }
                else
                {
                    _helper.ErrMessage("(ChatAndDataController): runningMission is not set.");
                    return null;
                }

                DataDrive dd = await _context.DataDrives.SingleOrDefaultAsync(x => x.MissionID == pMissionID && x.TeamID == teamID);

                if (dd == null)
                    return null;

                int currentRecord = dd.CurrentRecord;

                List<VolcanoData> volcData = new List<VolcanoData>();
                var webRoot = _env.WebRootPath;

                VolcanoData volcanoData = null;

                //Load the XML file in XmlDocument.
                string volcDataPath = webRoot + "\\App_Data\\rtVolcanoData.xml";
                XmlDocument volcDoc = new XmlDocument();
                volcDoc.Load(volcDataPath);

                //Loop through the selected Nodes.
                foreach (XmlNode node in volcDoc.SelectNodes("/advisories/advisory"))
                {
                    //Fetch the Node values and assign it to the Model.
                    volcData.Add(new VolcanoData
                    {
                        gmt = node["gmt"].InnerText,
                        vt = node["vt"].InnerText,
                        rf = node["rf"].InnerText,
                        obs = node["obs"].InnerText
                    });
                }

                volcanoData = volcData[currentRecord - 1];
                return volcanoData;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [HttpGet]
        public async Task<MedCommData> GetCurrentMCGMT(int pMissionID)
        {
            try
            {
                RunningMission runningMission = await _context.RunningMissions
                    .SingleOrDefaultAsync(x => x.MissionID == pMissionID);

                MissionEnding missionEnding = runningMission.MissionEnding;

                int teamID = await _context.Teams
                    .Where(t => t.MissionID == pMissionID && t.TeamName == (TeamName)4)
                    .Select(t => new int?(t.TeamID))
                    .SingleOrDefaultAsync() ?? -1;

                if (teamID == -1)
                {
                    return null;
                }

                int curRec = await _context.DataDrives
                    .Where(x => x.MissionID == pMissionID && x.TeamID == teamID)
                    .Select(s => s.CurrentRecord)
                    .SingleOrDefaultAsync();
                
                List<MedCommData> medcData = new List<MedCommData>();
                var webRoot = _env.WebRootPath;

                //Load the XML file in XmlDocument.
                string medcommDataPath = webRoot + "\\App_Data\\rtMedComData.xml";
                XmlDocument medcommDoc = new XmlDocument();
                medcommDoc.Load(medcommDataPath);

                //Loop through the selected Nodes.
                foreach (XmlNode node in medcommDoc.SelectNodes("/advisories/advisory"))
                {
                    //Fetch the Node values and assign it to the Model.
                    medcData.Add(new MedCommData
                    {
                        gmt = node["gmt"].InnerText,
                        alert = node["alert"].InnerText,
                        directives = node["directives"].InnerText,
                        airquality = new AirQuality()
                        {
                            south = node["airquality"].ChildNodes[0].InnerText,
                            north = node["airquality"].ChildNodes[1].InnerText
                        },
                        waterquality = new WaterQuality()
                        {
                            loc1 = new Reading()
                            {
                                location = node["waterquality"].ChildNodes[0].ChildNodes[0].InnerText,
                                ph = node["waterquality"].ChildNodes[0].ChildNodes[1].InnerText,
                                turbidity = node["waterquality"].ChildNodes[0].ChildNodes[2].InnerText,
                                contaminates = node["waterquality"].ChildNodes[0].ChildNodes[3].InnerText
                            },
                            loc2 = new Reading()
                            {
                                location = node["waterquality"].ChildNodes[1].ChildNodes[0].InnerText,
                                ph = node["waterquality"].ChildNodes[1].ChildNodes[1].InnerText,
                                turbidity = node["waterquality"].ChildNodes[1].ChildNodes[2].InnerText,
                                contaminates = node["waterquality"].ChildNodes[1].ChildNodes[3].InnerText
                            },
                            loc3 = new Reading()
                            {
                                location = node["waterquality"].ChildNodes[2].ChildNodes[0].InnerText,
                                ph = node["waterquality"].ChildNodes[2].ChildNodes[1].InnerText,
                                turbidity = node["waterquality"].ChildNodes[2].ChildNodes[2].InnerText,
                                contaminates = node["waterquality"].ChildNodes[2].ChildNodes[3].InnerText
                            },
                            loc4 = new Reading()
                            {
                                location = node["waterquality"].ChildNodes[3].ChildNodes[0].InnerText,
                                ph = node["waterquality"].ChildNodes[3].ChildNodes[1].InnerText,
                                turbidity = node["waterquality"].ChildNodes[3].ChildNodes[2].InnerText,
                                contaminates = node["waterquality"].ChildNodes[3].ChildNodes[3].InnerText
                            },
                            loc5 = new Reading()
                            {
                                location = node["waterquality"].ChildNodes[4].ChildNodes[0].InnerText,
                                ph = node["waterquality"].ChildNodes[4].ChildNodes[1].InnerText,
                                turbidity = node["waterquality"].ChildNodes[4].ChildNodes[2].InnerText,
                                contaminates = node["waterquality"].ChildNodes[4].ChildNodes[3].InnerText
                            }
                        }
                    });
                }

                MedCommData medcommData = medcData?[curRec - 1];
                if (medcommData != null)
                    return medcommData;
                else
                    return new MedCommData();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [HttpGet]
        public async Task<HurricaneData> GetCurrentADV(int pMissionID)
        {
            try
            {
                RunningMission runningMission = await _context.RunningMissions
                    .SingleOrDefaultAsync(x => x.MissionID == pMissionID);

                MissionEnding missionEnding = runningMission.MissionEnding;

                //TeamName team = (TeamName)pTeamID;

                Team theTeam = await _context.Teams
                    .OrderByDescending(o => o.TeamID)
                    .FirstOrDefaultAsync(x => x.MissionID == pMissionID && x.TeamName == (TeamName)1);

                int teamID = 0;

                if (theTeam != null)
                {
                    teamID = theTeam.TeamID;
                }
                else
                {
                    _helper.ErrMessage("(ChatAndDataController): theTeam is not set.");
                    return null;
                }

                DataDrive dd = await _context.DataDrives.SingleOrDefaultAsync(x => x.MissionID == pMissionID && x.TeamID == teamID);

                if (dd == null)
                {
                    return null;
                }

                int currentRecord = dd.CurrentRecord;

                List<HurricaneData> hurcData = new List<HurricaneData>();
                var webRoot = _env.WebRootPath;

                HurricaneData hurricaneData = null;

                //Load the XML file in XmlDocument.
                string hurcDataPath = (missionEnding == MissionEnding.Hurricane1) ? webRoot + "\\App_Data\\rtHurricaneData.xml" : webRoot + "\\App_Data\\altHurricaneData.xml";
                XmlDocument hurcDoc = new XmlDocument();
                hurcDoc.Load(hurcDataPath);

                //Loop through the selected Nodes.
                foreach (XmlNode node in hurcDoc.SelectNodes("/advisories/advisory"))
                {
                    //Fetch the Node values and assign it to the Model.
                    hurcData.Add(new HurricaneData
                    {
                        adv = node["adv"].InnerText,
                        lat = node["lat"].InnerText,
                        lon = node["lon"].InnerText,
                        gmt = node["gmt"].InnerText,
                        wind = node["wind"].InnerText,
                        pres = node["pres"].InnerText,
                        sat = node["sat"].InnerText,
                        leg = node["leg"].InnerText,
                        lbl1 = node["lbl1"].InnerText,
                        lbl2 = node["lbl2"].InnerText,
                        h = node["h"].InnerText
                    });
                }

                hurricaneData = hurcData[currentRecord - 1];
                return hurricaneData;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + ": " + e.StackTrace);
            }
            return null;
        }

        [HttpPost]
        public async Task<JsonResult> InsertDataDrive(DataDrive pDataDrive)
        {
            _context.DataDrives.Add(pDataDrive);
            await _context.SaveChangesAsync();
            return Json(pDataDrive);
        }

        [HttpPut]
        public async Task<JsonResult> UpdateDataDrive(DataDrive pDataDrive)
        {
            DataDrive objdDataDrive = await GetDataDrive(pDataDrive.MissionID, pDataDrive.TeamID);

            objdDataDrive.CurrentRecord = pDataDrive.CurrentRecord;
            objdDataDrive.CurrentTimerInterval = pDataDrive.CurrentTimerInterval;

            _context.DataDrives.Attach(objdDataDrive);
            _context.Entry(objdDataDrive).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Json(objdDataDrive);
        }

        [HttpPost]
        public async Task<JsonResult> InsertDataDriveData(DataDriveData pDataDriveData)
        {
            _context.DataDriveDatas.Add(pDataDriveData);
            await _context.SaveChangesAsync();
            return Json(pDataDriveData);
        }
    }
}