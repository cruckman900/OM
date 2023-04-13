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
    public class TeamDataDriveData : ViewComponent
    {
        private readonly OM2018Context _context;

        public TeamDataDriveData(OM2018Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int MissionID)
        {
            var data = from s in _context.DataDriveDatas
                       where s.MissionID == MissionID
                       select s;

            return View("TeamData", await data.AsNoTracking().ToListAsync());
        }
    }
}
