using Microsoft.AspNetCore.Mvc;
using OMNext.Data;
using System.Collections.Generic;

namespace OMNext.ViewComponents
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class Usage : ViewComponent
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        private readonly OM2018Context _context;

        public Usage(OM2018Context context)
        {
            _context = context;
        }

        public override bool Equals(object obj)
        {
            return obj is Usage usage &&
                   EqualityComparer<OM2018Context>.Default.Equals(_context, usage._context);
        }

        public IViewComponentResult Invoke()
        {
            return View("CLCUsage");
        }
    }
}
