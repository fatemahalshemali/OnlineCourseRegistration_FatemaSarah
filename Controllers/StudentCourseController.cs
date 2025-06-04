using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineCourseRegistration_FatemaSarah.Data;
using OnlineCourseRegistration_FatemaSarah.Models;

namespace OnlineCourseRegistration_FatemaSarah.Controllers
{
    public class StudentCourseController : Controller
    {
        private readonly AppDbContext db;

        public StudentCourseController(AppDbContext _db)
        {
            db = _db;
        }

        public async Task<IActionResult> Index()
        {
            var data = await db.StudentCourses
                .Include(sc => sc.Student)
                .Include(sc => sc.Course)
                .ToListAsync();

            return View(data);
        }

        public async Task<IActionResult> Enroll()
        {
            ViewBag.Students = new SelectList(await db.Students.ToListAsync(), "Id", "Name");
            ViewBag.Courses = new SelectList(await db.Courses.ToListAsync(), "Id", "Title");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Enroll(StudentCourse model)
        {
            if (ModelState.IsValid)
            {
                var exists = await db.StudentCourses
                    .AnyAsync(sc => sc.StudentId == model.StudentId && sc.CourseId == model.CourseId);

                if (exists)
                {
                    ModelState.AddModelError("", "Already registered for this course.");
                    return View(model);
                }

                db.StudentCourses.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Students = new SelectList(await db.Students.ToListAsync(), "Id", "Name");
            ViewBag.Courses = new SelectList(await db.Courses.ToListAsync(), "Id", "Title");
            return View(model);
        }

        public async Task<IActionResult> Approve(int studentId, int courseId)
        {
            var enrollment = await db.StudentCourses
                .FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId);

            if (enrollment != null)
            {
                enrollment.IsApproved = true;
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Reject(int studentId, int courseId)
        {
            var enrollment = await db.StudentCourses
                .FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId);

            if (enrollment != null)
            {
                db.StudentCourses.Remove(enrollment);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }


    }
}
