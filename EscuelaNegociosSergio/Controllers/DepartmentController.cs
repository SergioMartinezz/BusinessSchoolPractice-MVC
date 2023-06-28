using EscuelaNegociosSergio.Context;
using EscuelaNegociosSergio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EscuelaNegociosSergio.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly SchoolDbContext _db;

        public DepartmentController(SchoolDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult> AllDepartment()
        {
            var departments = await _db.Departments.ToListAsync();
            return View(departments);
        }

        public IActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            _db.Add(department);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllDepartment");
        }

        public async Task<IActionResult> EditDepartment(int id)
        {
            var department = await _db.Departments.FindAsync(id);
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> EditDepartment(Department department)
        {
            _db.Update(department);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllDepartment");
        }

        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _db.Departments.FindAsync(id);
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDepartment(Department department)
        {
            _db.Remove(department);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllDepartment");
        }

        public async Task<IActionResult> DepartmentDetails(int id)
        {
            Department department = await _db.Departments.SingleOrDefaultAsync(d => d.DepartmentId == id);
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDepartment(Department department)
        {
            return null;
        }
    }
}
