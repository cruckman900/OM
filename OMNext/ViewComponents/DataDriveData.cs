using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMNext.Data;
using OMNext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNext.ViewComponents
{
    public class DataDriveData : ViewComponent
    {
        private readonly OM2018Context _context;

        public DataDriveData(OM2018Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int MissionID, string Team)
        {
            DataMember dmTeam = new DataMember();
            if (Team == "Volcano")
            {
                dmTeam = DataMember.Volcano;
                ViewData["Team"] = "Volcano";
            }
            else if (Team == "Hurricane")
            {
                dmTeam = DataMember.Hurricane;
                ViewData["Team"] = "Hurricane";
            }
            else if (Team == "Evacuation")
            {
                dmTeam = DataMember.Evacuation;
                ViewData["Team"] = "Evacuation";
            }
            else if (Team == "MedComm")
            {
                dmTeam = DataMember.MedComm;
                ViewData["Team"] = "MedComm";
            }

            var data = from s in _context.DataDriveDatas
                       where s.MissionID == MissionID
                       && s.From == dmTeam
                       select s;

            return View("DataDriveData", await data.AsNoTracking().ToListAsync());
        }
    }
}
