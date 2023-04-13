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
    public class FDChat : ViewComponent
    {
        private readonly OM2018Context _context;

        public FDChat(OM2018Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int MissionID)
        {
            var chat = from s in _context.Chats
                            where s.MissionID == MissionID
                            select s;

            return View("Chat", await chat.AsNoTracking().ToListAsync());
        }
    }
}
