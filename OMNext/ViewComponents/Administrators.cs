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
    public class Administrators : ViewComponent
    {
        private readonly OM2018Context _context;

        public Administrators(OM2018Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var administators = from a in _context.Administrators
                          select a;

            return View("AdministratorsPage", await administators.AsNoTracking().ToListAsync());
        }
    }
}
