using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineCourseRegistration_FatemaSarah.Data;
using OnlineCourseRegistration_FatemaSarah.Models;

namespace OnlineCourseRegistration_FatemaSarah.Controllers
{
   [Authorize("Admin")]
    public class CourseController : Controller
    {
        private AppDbContext db;

        public CourseController(AppDbContext _db)
        {
            db = _db;
        }

        
        public async Task<IActionResult> Index()
        {
            var courses = await db.Courses.Include(c => c.Instructor).ToListAsync();
            return View(courses);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.InstructorId = new SelectList(await db.Instructors.ToListAsync(), "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.InstructorId = new SelectList(await db.Instructors.ToListAsync(), "Id", "FullName", course.InstructorId);
            return View(course);
        }


        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var course = await db.Courses.FindAsync(id);
            if (course == null)
                return View();

            ViewBag.InstructorId = new SelectList(await db.Instructors.ToListAsync(), "Id", "FullName", course.InstructorId);
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Update(course);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.InstructorId = new SelectList(await db.Instructors.ToListAsync(), "Id", "FullName", course.InstructorId);
            return View(course);
        }


        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {
            var course = await db.Courses
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(c => c.CourseId == id);
            if (course == null)
                return View();

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var course = await db.Courses.FindAsync(id);
            if (course != null)
            {
                db.Courses.Remove(course);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
     

       
        public async Task<IActionResult> Details(int id)
        {
            var course = await db.Courses
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(c => c.CourseId == id);

            if (course == null)
                return View();

            return View(course);
        }
     


    }


}
