using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMNext.Data;
using OMNext.Models;
using OMNext.Helpers;
using Microsoft.AspNetCore.Http;
using OMNext.Controllers;
using System.Security.Cryptography;

namespace OMNext.Controllers
{
    public class TeamsController : Controller
    {
        private readonly OM2018Context _context;
        private IWebHostEnvironment _env;

        /// <summary>
        /// Inject the database context
        /// </summary>
        /// <param name="context">The context to be used</param>
        public TeamsController(OM2018Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult TeamLogin()
        {
            ViewData["Team"] = "hide navbar links";
            ViewData["Message"] = "Team Login";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeamLogin(Team team)
        {
            HttpContext.Session.SetString("Password", team.Password);
            var runningMission = await _context.RunningMissions
                .OrderByDescending(o => o.MissionID).FirstOrDefaultAsync(s => s.Booth == team.Password);

            int ScriptID = (int)runningMission.ScriptID;
            ChatAndDataController ChatAndData = new ChatAndDataController(_context, _env);
            bool blnHasMedComm = await ChatAndData.HasMedComTeam(HttpContext.Session.GetString("Password"));

            TempData["HasMedComm"] = blnHasMedComm;

            if (runningMission != null)
            {
                team.MissionID = runningMission.MissionID;

                Team oTeam = HttpContext.Session.Get<Team>("Team");

                try
                {
                    if (ModelState.IsValid)
                    {
                        Helper h = new Helper();
                        Aes aes = Aes.Create();

                        // Check to see if team is in session,
                        // had to re-log from scratch
                        // or that they may be logging in as a different team.
                        if ((oTeam == null && team != null) || (oTeam.TeamName != team.TeamName))
                        {
                            byte[] password = h.EncryptString(runningMission.Booth, aes.Key, aes.IV);

                            Team chkTeam = await _context.Teams.OrderByDescending(o => o.TeamID)
                                .Where(x => (x.Password == runningMission.Booth || x.Password == password.ToString())
                                //.Where(x => (x.Password == runningMission.Booth)
                                && x.TeamName == team.TeamName
                                && x.MissionID == team.MissionID).FirstOrDefaultAsync();


                            team.Password = password.ToString();

                            if (chkTeam != null)
                            {
                                team = chkTeam;
                            }
                            else
                            {
                                _context.Add(team);
                                await _context.SaveChangesAsync();
                            }

                            HttpContext.Session.Set("Team", team);
                        }

                        HttpContext.Session.Set("MissionID", team.MissionID);

                        ////Go to the correct page for the team.
                        switch (team.TeamName)
                        {
                            case 0:
                                return RedirectToAction(nameof(Communications));
                            case (OMNext.Models.TeamName)1:
                                return RedirectToAction(nameof(Hurricane));
                            case (OMNext.Models.TeamName)2:
                                return RedirectToAction(nameof(Volcano));
                            case (OMNext.Models.TeamName)3:
                                return RedirectToAction(nameof(Evacuation));
                            case (OMNext.Models.TeamName)4:
                                return RedirectToAction(nameof(MedComm));
                        }
                    }
                }
                catch (Exception /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator");
                }
            }
            return RedirectToAction(nameof(TeamLogin));
        }

        public IActionResult Communications()
        {
            if (HttpContext.Session.Get<Team>("Team") != null)
            {
                ViewData["Message"] = "Communications Team Interface";
                ViewData["Team"] = "Communications";
                ViewData["MissionID"] = HttpContext.Session.Get<Team>("Team").MissionID;
                ViewData["Password"] = HttpContext.Session.GetString("Password");
                

                var webRoot = _env.WebRootPath;
                var PostBriefFile = System.IO.Path.Combine(webRoot, "App_Data/commPostbrief.txt");

                ViewBag.PostBrief = System.IO.File.ReadAllText(PostBriefFile);

                return View();
            }
            else
            {
                return RedirectToAction(nameof(TeamLogin));
            }
        }

        public IActionResult Volcano()
        {
            if (HttpContext.Session.Get<Team>("Team") != null)
            {
                ViewData["Message"] = "Volcano Team Interface";
                ViewData["Team"] = "Volcano";
                ViewData["TeamID"] = HttpContext.Session.Get<Team>("Team").TeamID;
                ViewData["MissionID"] = HttpContext.Session.Get<Team>("Team").MissionID;
                ViewData["Password"] = HttpContext.Session.GetString("Password");

                var webRoot = _env.WebRootPath;
                var ArchivedVolDataFile = System.IO.Path.Combine(webRoot, "App_Data/archivedVolData.txt");
                var PostBriefFile = System.IO.Path.Combine(webRoot, "App_Data/volcPostbrief.txt");

                ViewBag.ArchivedVolData = System.IO.File.ReadAllText(ArchivedVolDataFile);
                ViewBag.PostBrief = System.IO.File.ReadAllText(PostBriefFile);

                return View();
            }
            else
            {
                return RedirectToAction(nameof(TeamLogin));
            }
        }

        public IActionResult Hurricane()
        {
            if (HttpContext.Session.Get<Team>("Team") != null)
            {
                ViewData["Message"] = "Hurricane Team Interface";
                ViewData["Team"] = "Hurricane";
                ViewData["MissionID"] = HttpContext.Session.Get<Team>("Team").MissionID;
                ViewData["Password"] = HttpContext.Session.GetString("Password");

                var webRoot = _env.WebRootPath;
                var ArchivedHurDataFile = System.IO.Path.Combine(webRoot, "App_Data/archivedHurData.txt");
                var PostBriefFile = System.IO.Path.Combine(webRoot, "App_Data/hurcPostbrief.txt");

                ViewBag.ArchivedHurData = System.IO.File.ReadAllText(ArchivedHurDataFile);
                ViewBag.PostBrief = System.IO.File.ReadAllText(PostBriefFile);

                return View();
            }
            else
            {
                return RedirectToAction(nameof(TeamLogin));
            }
        }

        public IActionResult Evacuation()
        {
            if (HttpContext.Session.Get<Team>("Team") != null)
            {
                ViewData["Message"] = "Evacuation Team Interface";
                ViewData["Team"] = "Evacuation";
                ViewData["MissionID"] = HttpContext.Session.Get<Team>("Team").MissionID;
                ViewData["Password"] = HttpContext.Session.GetString("Password");

                var webRoot = _env.WebRootPath;
                var Shelters = System.IO.Path.Combine(webRoot, "App_Data/evacShelters.txt");
                var PostBriefFile = System.IO.Path.Combine(webRoot, "App_Data/evacPostbrief.txt");

                ViewBag.EvacShelters = System.IO.File.ReadAllText(Shelters);
                ViewBag.PostBrief = System.IO.File.ReadAllText(PostBriefFile);

                return View();
            }
            else
            {
                return RedirectToAction(nameof(TeamLogin));
            }
        }

        public IActionResult MedComm()
        {
            if (HttpContext.Session.Get<Team>("Team") != null)
            {
                ViewData["Message"] = "Med Comm Team Interface";
                ViewData["Team"] = "MedComm";
                ViewData["MissionID"] = HttpContext.Session.Get<Team>("Team").MissionID;
                ViewData["Password"] = HttpContext.Session.GetString("Password");

                var webRoot = _env.WebRootPath;
                var Briefing = System.IO.Path.Combine(webRoot, "App_Data/medcommBriefing.txt");
                var Bulletin = System.IO.Path.Combine(webRoot, "App_Data/medcommBulletin.txt");
                var PostBriefFile = System.IO.Path.Combine(webRoot, "App_Data/medcommPostbrief.txt");

                ViewBag.Briefing = System.IO.File.ReadAllText(Briefing);
                ViewBag.Bulletin = System.IO.File.ReadAllText(Bulletin);
                ViewBag.PostBrief = System.IO.File.ReadAllText(PostBriefFile);

                return View();
            }
            else
            {
                return RedirectToAction(nameof(TeamLogin));
            }
        }
    }
}