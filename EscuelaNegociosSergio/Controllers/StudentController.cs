using EscuelaNegociosSergio.Context;
using EscuelaNegociosSergio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EscuelaNegociosSergio.Controllers
{
    public class StudentController : Controller
    {
        private readonly SchoolDbContext _db;
        public StudentController(SchoolDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult> AllStudent()
        {
            var students = await _db.Students.ToListAsync();
            return View(students);
        }

        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            _db.Add(student);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllStudent");
        }

        public async Task<IActionResult> EditStudent (int id)
        {
            Student student;
            student = _db.Students.Find(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(Student student)
        {
            _db.Update(student);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllStudent");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _db.Students.FindAsync(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStudent(Student student)
        {
            _db.Remove(student);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllStudent");
        }

        public async Task<IActionResult> EnrollClub(int? id)
        {
            var studentDisplay = await _db.Students.Select(x => new
            {
                Id = x.StudentId,
                Value = x.StudentName
            }).ToListAsync();

            ViewModels.StudentEnrollmentViewModel vm = new ViewModels.StudentEnrollmentViewModel();
            vm.StudentList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(studentDisplay, "Id", "Value");
            var club = await _db.Clubs.SingleOrDefaultAsync(c => c.ClubId == id);
            ViewBag.Club = club;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EnrollClub(ViewModels.StudentEnrollmentViewModel vm)
        {
            if (Comprueba(vm.Enrollment.Club.ClubId, vm.Enrollment.Student.StudentId))
            {
                return RedirectToAction("Information");
            }
            else
            {
                var student = await _db.Students.SingleOrDefaultAsync(s => s.StudentId == vm.Enrollment.Student.StudentId);
                var club = await _db.Clubs.SingleOrDefaultAsync(s => s.ClubId == vm.Enrollment.Club.ClubId);

                club.StudentsNumber++;

                Enrollment enrollment = new Enrollment();
                enrollment.Student = student;
                enrollment.Club = club;
                _db.Add(enrollment);

                await _db.SaveChangesAsync();
                return RedirectToAction("AllClub", "Club");
            }
        }
        private bool Comprueba(int clubId, int studentId)
        {
            var enrollment = _db.Enrollments.Include(c => c.Student).Include(c=>c.Club).Where(c => c.Club.ClubId == clubId && c.Student.StudentId == studentId).FirstOrDefault();
            bool located = (enrollment != null);
            return located;
        }

        public IActionResult Information()
        {
            return View();
        }

        public async Task<IActionResult> AllInscribed(int? id)
        {
            var enrollment = await _db.Enrollments.Where(e => e.Club.ClubId == id).Include(e => e.Club).Include(e => e.Student).ToListAsync();

            List<Student> inscribed = new List<Student>();
            foreach (var e in enrollment)
            {
                var student = await _db.Students.SingleOrDefaultAsync(s => s.StudentId == e.Student.StudentId);
                inscribed.Add(student);
            }

            ViewData["Club"] = _db.Clubs.Find(id).ClubName;
            return View(inscribed);
        }
    }
}
