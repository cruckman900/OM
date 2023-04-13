using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMNext.Data;
using OMNext.Models;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace OMNext.ViewComponents
{
    public class FDDataPush : ViewComponent
    {
        private readonly OM2018Context _context;
        private IWebHostEnvironment _env;

        public FDDataPush(OM2018Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IViewComponentResult> InvokeAsync(int MissionID, int TeamID)
        {
            var datadrive = from s in _context.DataDrives
                            where s.MissionID == MissionID
                            && s.TeamID == TeamID
                            select s;

            if (MissionID == 0)
            {
                datadrive = null;
                return View("DataPush", new DataDrive());
            }

            //Get the mission ending for hurricane data drive
            var missionver = from m in _context.RunningMissions
                             where m.MissionID == MissionID
                             select m.MissionEnding;

            string webRoot = _env.WebRootPath;

            if (TeamID == 5)
            {
                // TODO: not sure we need to do anything here yet.
            } else {
                switch (TeamID)
                {
                    case 0: //Communications
                    case 3: //Evacuation
                        break;
                    case 1:
                        {
                            //Hurricane
                            List<HurricaneData> hurcData = new List<HurricaneData>();

                            string hurcDataPath = null;

                            //Load the XML file in XmlDocument
                            switch (missionver.FirstOrDefault())
                            {
                                case MissionEnding.Hurricane1:
                                    hurcDataPath = webRoot + "\\App_Data\\rtHurricaneData.xml";
                                    break;
                                case MissionEnding.Hurricane2:
                                    hurcDataPath = webRoot + "\\App_Data\\altHurricaneData.xml";
                                    break;
                                default:
                                    break;
                            }

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
                            ViewBag.HurcData = hurcData;
                            break;
                        }
                    case 2:
                        { //Volcano
                            List<VolcanoData> volcData = new List<VolcanoData>();

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
                            //volcData.Reverse();
                            ViewBag.VolcData = volcData;
                            break;
                        }
                    case 4:
                        { //MedComm
                            List<MedCommData> medcomData = new List<MedCommData>();

                            string medcomDataPath = webRoot + "\\App_Data\\rtMedComData.xml";

                            XmlDocument medcomDoc = new XmlDocument();
                            medcomDoc.Load(medcomDataPath);

                            foreach (XmlNode node in medcomDoc.SelectNodes("/advisories/advisory"))
                            {
                                //Fetch the Node values and assign it to the Model.
                                medcomData.Add(new MedCommData
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
                            ViewBag.MedComData = medcomData;
                            break;
                        }
                    default:
                        break;
                }

                return View("DataPush", await datadrive.AsNoTracking().ToListAsync());
            }
            return View("DataPush");
        }
    }
}
