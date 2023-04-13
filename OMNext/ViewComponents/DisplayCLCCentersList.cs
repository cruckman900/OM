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
    public class DisplayCLCCentersList : ViewComponent
    {
        private readonly OM2018Context _context;

        public DisplayCLCCentersList(OM2018Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var clccenters = await _context.CLCCenters.ToListAsync();

            return View("GetCLCCenters", clccenters);
        }
    }
}
