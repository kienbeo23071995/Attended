using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Group01_PRJ.Models;

namespace Group01_PRJ.Controllers
{
    public class UserClassesController : Controller
    {
        private readonly AttendedContext _context;

        public UserClassesController(AttendedContext context)
        {
            _context = context;
        }

        // GET: UserClasses
        public async Task<IActionResult> Index(int? Classid, int? Courseid)
        {
            ViewData["Classid"] = new SelectList(_context.Classes, "Id", "Name", Classid);
            ViewData["Courseid"] = new SelectList(_context.Courses, "Id", "Name", Courseid);
            var attendedContext = _context.UserClasses
                .Include(u => u.Class)
                .Include(u => u.Course)
                .Include(u => u.User)
                .Where(u => (u.Classid == Classid || Classid==null) && (u.Courseid == Courseid || Courseid==null));
            return View(await attendedContext.ToListAsync());
        }

        // GET: UserClasses/Details/5
        public async Task<IActionResult> Details(int? classId, int? courseId, int? userId)
        {
            if (classId == null || courseId == null || userId == null)
            {
                return NotFound();
            }

            var userClass = await _context.UserClasses
                .Include(u => u.Class)
                .Include(u => u.Course)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Classid == classId && m.Courseid==courseId && m.Userid==userId);
            if (userClass == null)
            {
                return NotFound();
            }

            return View(userClass);
        }

        // GET: UserClasses/Create
        public IActionResult Create()
        {
            ViewData["Classid"] = new SelectList(_context.Classes, "Id", "Name");
            ViewData["Courseid"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: UserClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Classid,Userid,Courseid")] UserClass userClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Classid"] = new SelectList(_context.Classes, "Id", "Name", userClass.Classid);
            ViewData["Courseid"] = new SelectList(_context.Courses, "Id", "Name", userClass.Courseid);
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Email", userClass.Userid);
            return View(userClass);
        }

        // GET: UserClasses/Edit/5
        public async Task<IActionResult> Edit(int? classId, int? courseId, int? userId)
        {
            if (classId == null || courseId == null || userId == null)
            {
                return NotFound();
            }

            var userClass = await _context.UserClasses.FirstOrDefaultAsync(m => m.Classid == classId && m.Courseid == courseId && m.Userid == userId);
            if (userClass == null)
            {
                return NotFound();
            }
            ViewData["Classid"] = new SelectList(_context.Classes, "Id", "Name", userClass.Classid);
            ViewData["Courseid"] = new SelectList(_context.Courses, "Id", "Name", userClass.Courseid);
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Email", userClass.Userid);
            return View(userClass);
        }

        // POST: UserClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? classId, int? courseId, int? userId, [Bind("Classid,Userid,Courseid")] UserClass userClass)
        {
            if (classId != userClass.Classid || courseId != userClass.Courseid || userId!=userClass.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserClassExists(userClass.Classid, userClass.Courseid, userClass.Userid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Classid"] = new SelectList(_context.Classes, "Id", "Name", userClass.Classid);
            ViewData["Courseid"] = new SelectList(_context.Courses, "Id", "Name", userClass.Courseid);
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Email", userClass.Userid);
            return View(userClass);
        }

        // GET: UserClasses/Delete/5
        public async Task<IActionResult> Delete(int? classId, int? courseId, int? userId)
        {
            if (classId == null || courseId == null || userId == null)
            {
                return NotFound();
            }

            var userClass = await _context.UserClasses
                .Include(u => u.Class)
                .Include(u => u.Course)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Classid == classId && m.Courseid == courseId && m.Userid == userId);
            if (userClass == null)
            {
                return NotFound();
            }

            return View(userClass);
        }

        // POST: UserClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? classId, int? courseId, int? userId)
        {
            var userClass = await _context.UserClasses.FirstAsync(m=> m.Classid == classId && m.Courseid == courseId && m.Userid == userId);
            _context.UserClasses.Remove(userClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserClassExists(int? classId, int? courseId, int? userId)
        {
            return _context.UserClasses.Any(m => m.Classid == classId && m.Courseid == courseId && m.Userid == userId);
        }
    }
}
