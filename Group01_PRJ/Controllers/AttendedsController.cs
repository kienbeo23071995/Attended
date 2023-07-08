using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Group01_PRJ.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Group01_PRJ.Controllers
{
    public class AttendedsController : Controller
    {
        private readonly AttendedContext _context;

        public AttendedsController(AttendedContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(DateTime? date)
        {
            var json = HttpContext.Session.GetString("user");
            User user = json != null ? JsonConvert.DeserializeObject<User>(json) : null;
            if(user == null) return NotFound();
            var attendedContext = _context.Sessions
                .Include(s => s.Class)
                .Include(s => s.Course)
                .Include(s => s.Room)
                .Include(s => s.Slot)
                .Include(s => s.User)
                .Where(item => (item.Date==date || item.Date == new DateTime()) 
                && (item.Userid == user.Id  || user.CheckGroup("student") || user.CheckGroup("admin") || user == null));
            ViewBag.date = date ?? DateTime.Now;
            return View(await attendedContext.ToListAsync());
        }

        public IActionResult Attend(int roomId, int slotId, DateTime date)
        {
            Session session = _context.Sessions
                .Include(u => u.Class)
                .Include(u => u.Course)
                .Include(u => u.User)
                .Include(u => u.Slot)
                .Include(u => u.Attendeds)
                .FirstOrDefault(item => (item.Roomid.Equals(roomId)
                && item.Slotid.Equals(slotId)
                && item.Date.Equals(date)));
            if(session == null)
            {
                return NotFound();
            }
            ViewBag.session = session;
            List<UserClass> userClass = _context.UserClasses
                .Include(u => u.Class)
                .Include(u => u.Course)
                .Include(u => u.User)
                .Where(m => m.Classid == session.Classid && m.Courseid == session.Courseid).ToList();
            ViewBag.userClasses = userClass;
            return View();
        }

        [HttpPost]
        public IActionResult Attend(int roomId, int slotId, DateTime date, [Bind("attends")] bool[]? attends)
        {
            if(attends == null)
            {
                return NotFound();
            }
            Session session = _context.Sessions
                .Include(u => u.Class)
                .Include(u => u.Course)
                .Include(u => u.User)
                .Include(u => u.Slot)
                .FirstOrDefault(item => (item.Roomid.Equals(roomId)
                && item.Slotid.Equals(slotId)
                && item.Date.Equals(date)));
            if (session == null)
            {
                return NotFound();
            }
            ViewBag.session = session;
            List<UserClass> userClass = _context.UserClasses
                .Include(u => u.Class)
                .Include(u => u.Course)
                .Include(u => u.User)
                .Where(m => m.Classid == session.Classid && m.Courseid == session.Courseid).ToList();
            ViewBag.userClasses = userClass;
            if(userClass.Count != attends.Length)
            {
                return NotFound();
            }
            List<Attended> attendeds = _context.Attendeds.Where(a => a.Roomid == session.Roomid && a.Slotid == session.Slotid && a.Date == session.Date).ToList();
            if(attendeds!=null && attendeds.Count > 0)
            {
                _context.Attendeds.RemoveRange(attendeds);
                _context.SaveChanges();
            }
            for (int i = 0; i < userClass.Count; i++)
            {
                
                _context.Attendeds.Add(new Attended
                {
                    Roomid = session.Roomid,
                    Slotid = session.Slotid,
                    Date = session.Date,
                    Userid = userClass[i].Userid,
                    Status = attends[i]
                }) ;
            }
            _context.SaveChanges();
            return View();
        }
    }
}
