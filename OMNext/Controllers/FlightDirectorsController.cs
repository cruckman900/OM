using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OMNext.Data;
using OMNext.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OMNext.Helpers;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

namespace OMNext.Controllers
{
    public class FlightDirectorsController : Controller
    {
        private readonly OM2018Context _context;

        /// <summary>
        /// Inject the database context
        /// </summary>
        /// <param name="context">The context to be used</param>
        public FlightDirectorsController(OM2018Context context)
        {
            _context = context;
        }

        /// <summary>
        /// The login page.
        /// </summary>
        /// <returns></returns>
        public IActionResult FDLogin()
        {
            if (HttpContext.Session.Get<bool>("CLCCenterMatches") == false)
            {
                ViewData["Message"] = "You must provide a valid CLC Center ID";
            }
            else
            {
                ViewData["Message"] = "Create New Mission.";
            }

            ViewBag.Scripts = _context.Scripts.OrderBy(x => x.Title).AsEnumerable().Select(s => new SelectListItem { Text = s.Title, Value = s.ScriptID.ToString() });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FDLogin(string CLCCenter, RunningMission runningMission)
        {
            var clcCenter = await _context.CLCCenters
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.Abbreviation == CLCCenter);

            if (clcCenter == null)
            {
                HttpContext.Session.Set("CLCCenterMatches", false);
                return RedirectToAction(nameof(FDLogin));
            }
            else
            {
                runningMission.CLCCenterID = clcCenter.CLCCenterID;
                runningMission.MissionDate = DateTime.Now;
                //byte[] pass = runningMission.Booth;

                //Helper h = new Helper();
                //Aes aes = Aes.Create();
                //byte[] booth = h.EncryptString(runningMission.Booth, aes.Key, aes.IV);

                //runningMission.Booth = booth;

                try
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(runningMission);
                        await _context.SaveChangesAsync();

                        RunningMission rm = await _context.RunningMissions
                            .OrderByDescending(x => x.MissionID)
                            .FirstOrDefaultAsync(s => s.FlightDirector == runningMission.FlightDirector
                            && s.Booth == runningMission.Booth
                            && s.CLCCenterID == runningMission.CLCCenterID);

                        HttpContext.Session.Set("CurrentRunningMission", rm);

                        HttpContext.Session.Set("MissionID", rm.MissionID);

                        ViewData["MissionID"] = rm.MissionID;

                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.Message.ToString());
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator");
                }
                return RedirectToAction(nameof(FDLogin));
            }
        }

        /// <summary>
        /// The relog page.
        /// </summary>
        /// <returns></returns>
        public IActionResult FDRelog()
        {
            ViewData["Message"] = "Return to an Existing Mission";

            ViewBag.RunningMissions = _context.RunningMissions.OrderByDescending(x => x.MissionID).AsEnumerable()
                .Where(w => w.MissionDate > DateTime.Now.AddHours(-2))
                .Select(s => new SelectListItem { Text = s.MissionDate + ": " + s.FlightDirector, Value = s.MissionID.ToString() });

            if (HttpContext.Session.Get<RunningMission>("CurrentRunningMission") != null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FDRelog(int MissionID, string Booth)
        {
            try
            {
                RunningMission rm = await _context.RunningMissions
                    .FirstOrDefaultAsync(s => s.MissionID == MissionID && s.Booth == Booth);

                if (rm != null)
                {
                    HttpContext.Session.Set("CurrentRunningMission", rm);

                    HttpContext.Session.Set("MissionID", rm.MissionID);

                    ViewData["MissionID"] = rm.MissionID;

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator");
            }
            return RedirectToAction(nameof(FDRelog));
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                HttpContext.Session.Set("CLCCenterMatches", true);
                ViewData["Message"] = "Flight Director Interface";

                RunningMission rm2 = HttpContext.Session.Get<RunningMission>("CurrentRunningMission");

                using (ChatAndDataController cnd = new ChatAndDataController(_context, null))
                {
                    bool hasMedComm = await cnd.HasMedComTeam(rm2.Booth);
                    TempData["HasMedComm"] = hasMedComm;
                }

                ViewData["MissionID"] = rm2.MissionID;

                Script script = await _context.Scripts.FirstOrDefaultAsync(x => x.ScriptID == rm2.ScriptID);
                ViewData["Script"] = script.ScriptBody;
                return View();
            }
            catch (Exception)
            {
                Console.WriteLine("Error Starting Mission.");
            }

            return null;
        }
    }
}