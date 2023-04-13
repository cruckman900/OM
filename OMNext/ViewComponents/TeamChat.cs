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
    public class TeamChat : ViewComponent
    {
        private readonly OM2018Context _context;

        public TeamChat(OM2018Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int MissionID)
        {
            IQueryable<Chat> chat = from s in _context.Chats
                       where s.MissionID == MissionID
                       orderby s.MissionID, s.SentDateTime descending
                       select s;

            return View("TeamChat", await chat.AsNoTracking().ToListAsync());
        }
    }
}