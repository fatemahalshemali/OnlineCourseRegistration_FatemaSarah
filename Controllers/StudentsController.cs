using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseRegistration_FatemaSarah.Data;
using OnlineCourseRegistration_FatemaSarah.Models;

namespace OnlineCourseRegistration_FatemaSarah.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentsController : Controller
    {
        private AppDbContext db;

        public StudentsController(AppDbContext _db)
        {
            db = _db;
        }

        
        public IActionResult Index()
        {
            var students = db.Students.ToList();
            return View(students);
        }
        
        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
        
        [HttpGet]

        public IActionResult Edit(int id)
        {
            var student = db.Students.Find(id);
            if (student == null) return View();
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Update(student);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var stuData = db.Students.Find(id);
            if (stuData != null)
            {
                return View(stuData);

            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ConfirmDelete(Student student)
        {
            var stuData = db.Students.Find(student.StudentId);

            if (stuData != null)
            {
                db.Students.Remove(stuData);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));

        }
    }


}
