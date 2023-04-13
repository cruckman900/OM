using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OMNext.Models;
using OMNext.Data;
using System.Collections;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using System.Diagnostics;

namespace OMNext.Controllers
{
    public class AdministratorsController : Controller
    {
        private readonly OM2018Context _context;

        /// <summary>
        /// Inject the database context
        /// </summary>
        /// <param name="context">The context to be used</param>
        public AdministratorsController(OM2018Context context)
        {
            _context = context;
        }

        /// <summary>
        /// The login page.
        /// </summary>
        /// <returns>View</returns>
        public IActionResult AdminLogin()
        {
            ViewData["Message"] = "You must be logged in to use Administrative functions.";
            return View();
        }

        /// <summary>
        /// Check to see if user exists.  If yes, go to the administrator interface,
        /// else return to the login page.
        /// </summary>
        /// <param name="administrator"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdminLogin([Bind("UserName", "Password")] Administrator administrator)
        {
            try
            {
                var admin = _context.Administrators.Where(x => x.UserName == administrator.UserName
                    && x.Password == administrator.Password).FirstOrDefault();

                if (admin != null)
                {
                    HttpContext.Session.SetString("IsAdmin", "true");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return AdminLogin();
            }
        }

        /// <summary>
        /// Shows the administrator interface
        /// </summary>
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                ViewData["Title"] = "Administrator Page";
                ViewData["Message"] = "Administrator Page";
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }

        #region Admins

        /// <summary>
        /// Create a new Administrator account page
        /// </summary>
        public IActionResult AddAdministrator()
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                ViewData["Title"] = "Add a New Administrator";
                ViewData["Message"] = "Use this page to add a new Administrator";
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }

        /// <summary>
        /// Add the new Administrator to the database
        /// </summary>
        /// <param name="administrator">Administrator object to be added</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin([Bind("FirstName", "LastName", "UserName", "Password")] Administrator administrator)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(administrator);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "See your system administrator.");
            }
            return View(administrator);
        }

        /// <summary>
        /// Display the editor for an Administrator Account
        /// </summary>
        /// <param name="id">The Administrator's ID</param>
        public IActionResult EditAdministrator(int? id)
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                ViewData["Title"] = "Edit an Existing Administrator";
                ViewData["Message"] = "Use this page to edit an existing Administrator.";

                if (id == null)
                {
                    return NotFound();
                }

                var admin = _context.Administrators.SingleOrDefault(s => s.AdministratorID == id);
                if (admin == null)
                {
                    return NotFound();
                }

                return View(admin);
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifyAdministrator(int? administratorID)
        {
            if (administratorID == null)
            {
                return NotFound();
            }

            var adminToUpdate = _context.Administrators.SingleOrDefault(s => s.AdministratorID == administratorID);
            if (await TryUpdateModelAsync<Administrator>(adminToUpdate,
                    "",
                    s => s.FirstName,
                    s => s.LastName,
                    s => s.UserName,
                    s => s.Password))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator");
                }
            }
            return View(adminToUpdate);
        }

        public async Task<IActionResult> DeleteAdministrator(int? id, bool? saveChangesError = false)
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var admin = await _context.Administrators
                    .AsNoTracking()
                    .SingleOrDefaultAsync(s => s.AdministratorID == id);

                if (admin == null)
                {
                    return NotFound();
                }

                if (saveChangesError.GetValueOrDefault())
                {
                    ViewData["ErrorMessage"] =
                        "Delete failed. Try again, and if the problem persists " +
                        "see your system administrator.";
                }

                return View(admin);
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }

        [HttpPost, ActionName("DeleteAdministrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAdminConfirmed(int administratorID)
        {
            var admin = await _context.Administrators
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.AdministratorID == administratorID);

            if (admin == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Administrators.Remove(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(DeleteAdministrator), new { id = administratorID, saveChangesError = true });
            }
        }

        #endregion Admins

        #region Scripts

        public IActionResult AddScript()
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                ViewData["Title"] = "Add a New Script";
                ViewData["Message"] = "Use this page to add a new Flight Director Script.";
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateScript([Bind("Title, ScriptBody, CreatedOnDate, LastModifiedDate")] Script script)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(script);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "See your system administrator.");
            }
            return View(script);
        }

        public IActionResult EditScript(int? id)
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                ViewData["Title"] = "Edit an Existing Script";
                ViewData["Message"] = "Use this page to edit an existing Flight Director Script.";

                if (id == null)
                {
                    return NotFound();
                }

                var script = _context.Scripts.SingleOrDefault(s => s.ScriptID == id);
                if (script == null)
                {
                    return NotFound();
                }

                return View(script);
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifyScript(int? scriptID)
        {
            if (scriptID == null)
            {
                return NotFound();
            }

            var scriptToUpdate = _context.Scripts.SingleOrDefault(s => s.ScriptID == scriptID);
            if (await TryUpdateModelAsync<Script>(scriptToUpdate,
                    "",
                    s => s.Title, 
                    s => s.ScriptBody, 
                    s => s.CreatedOnDate, 
                    s => s.LastModifiedDate))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator");
                }
            }
            return View(scriptToUpdate);
        }

        public async Task<IActionResult> DeleteScript(int? id, bool? saveChangesError = false)
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var script = await _context.Scripts
                    .AsNoTracking()
                    .SingleOrDefaultAsync(s => s.ScriptID == id);

                if (script == null)
                {
                    return NotFound();
                }

                if (saveChangesError.GetValueOrDefault())
                {
                    ViewData["ErrorMessage"] =
                        "Delete failed. Try again, and if the problem persists " +
                        "see your system administrator.";
                }

                return View(script);
            }
            else
            {
                return RedirectToAction("IsAdmin");
            }
        }

        [HttpPost, ActionName("DeleteScript")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteScriptConfirmed(int scriptID)
        {
            var script = await _context.Scripts
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.ScriptID == scriptID);

            if (script == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Scripts.Remove(script);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(DeleteScript), new { id = scriptID, saveChangesError = true });
            }
        }

        #endregion Scripts

        #region CLC Centers

        public IActionResult AddCLCCenter()
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                ViewData["Title"] = "Add a New CLC Center";
                ViewData["Message"] = "Use this page to add a new Challenger Learning Center.";
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCLCCenter([Bind("CenterName, Abbreviation, Address1, Address2, City, State, Zipcode, Phone, IsActive, HasAccessToMedComm")] CLCCenter center)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(center);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "See your system administrator.");
            }
            return View(center);
        }

        public IActionResult EditCLCCenter(int? id)
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                ViewData["Title"] = "Edit an Existing CLC Center";
                ViewData["Message"] = "Use this page to edit an existing Challenger Learning Center";

                if (id == null)
                {
                    return NotFound();
                }

                var center = _context.CLCCenters.SingleOrDefault(s => s.CLCCenterID == id);
                if (center == null)
                {
                    return NotFound();
                }

                return View(center);
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifyCLCCenter(int? CLCCenterID)
        {
            if (CLCCenterID == null)
            {
                return NotFound();
            }

            var centerToUpdate = _context.CLCCenters.SingleOrDefault(c => c.CLCCenterID == CLCCenterID);
            if (await TryUpdateModelAsync<CLCCenter>(centerToUpdate,
                    "", 
                    c => c.CenterName,
                    c => c.Abbreviation,
                    c => c.Address1,
                    c => c.Address2,
                    c => c.City,
                    c => c.State,
                    c => c.Zipcode,
                    c => c.Phone,
                    c => c.IsActive,
                    c => c.HasAccessToMedComm))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator");
                }
            }
            return View(centerToUpdate);
        }

        public async Task<IActionResult> DeleteCLCCenter(int? id, bool? saveChangesError = false)
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var center = await _context.CLCCenters
                    .AsNoTracking()
                    .SingleOrDefaultAsync(s => s.CLCCenterID == id);

                if (center == null)
                {
                    return NotFound();
                }

                if (saveChangesError.GetValueOrDefault())
                {
                    ViewData["ErrorMessage"] =
                        "Delete failed. Try again, and if the problem persists " +
                        "see your system administrator.";
                }

                return View(center);
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }

        [HttpPost, ActionName("DeleteCLCCenter")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCLCCenterConfirmed(int clcCenterID)
        {
            var center = await _context.CLCCenters
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.CLCCenterID == clcCenterID);

            if (center == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.CLCCenters.Remove(center);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(DeleteCLCCenter), new { clcCenterID, saveChangesError = true });
            }
        }

        #endregion CLC Centers

        #region Usage

        public IActionResult Usage()
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                ViewData["Title"] = "Site Usage";
                ViewData["Message"] = "Site Usage";
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DisplayCLCUsage(DateTime startDate, DateTime endDate)
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                var usage = await _context.RunningMissions
                    .Include(b => b.CLCCenter)
                    .Where(x => x.MissionDate > startDate && x.MissionDate < endDate.AddDays(1))
                    .OrderBy(x => x.CLCCenterID).ThenBy(y => y.MissionDate)
                    .ToListAsync();

                ViewBag.StartDate = startDate.ToShortDateString();
                ViewBag.EndDate = endDate.ToShortDateString();

                return View(usage);
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }

        public async Task<IActionResult> GetCLCCenters()
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                var centers = await _context.CLCCenters
                    .OrderBy(x => x.Abbreviation)
                    .ToListAsync();

                return View(centers);
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }

        #endregion Usage
    }
}