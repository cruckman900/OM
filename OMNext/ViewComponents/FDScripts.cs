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
    public class FDScripts : ViewComponent
    {
        private readonly OM2018Context _context;

        public FDScripts(OM2018Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var scripts = from s in _context.Scripts
                          select s;

            return View("ScriptPage", await scripts.AsNoTracking().ToListAsync());
        }
    }
}
