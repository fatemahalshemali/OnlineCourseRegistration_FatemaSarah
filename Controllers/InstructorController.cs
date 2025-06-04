using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourseRegistration_FatemaSarah.Data;
using OnlineCourseRegistration_FatemaSarah.Models;

namespace OnlineCourseRegistration_FatemaSarah.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InstructorController : Controller
    {

        private AppDbContext db;

        public InstructorController(AppDbContext _db)
        {
            db = _db;
        }

        public async Task<IActionResult> Index()
        {
            var instructors = await db.Instructors.ToListAsync();
            return View(instructors);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                db.Instructors.Add(instructor);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var instructor = await db.Instructors.FindAsync(id);
            if (instructor == null)
                return View();

            return View(instructor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Instructor model)
        {
            if (ModelState.IsValid)
            {
                var instructor = await db.Instructors.FindAsync(model.InstructorId);
                if (instructor == null)
                    return View();

                // Update fields
                instructor.FullName = model.FullName;
                instructor.Email = model.Email;
                instructor.Major = model.Major;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return View();

            var instructor = await db.Instructors
                .FirstOrDefaultAsync(i => i.InstructorId == id);

            if (instructor == null) return View();

            return View(instructor);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(Instructor instructor)
        {
            var insData = await db.Instructors.FindAsync(instructor.InstructorId);

            if (insData != null)
            {
                db.Instructors.Remove(insData);
                await db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

