using EscuelaNegociosSergio.Context;
using EscuelaNegociosSergio.Models;
using EscuelaNegociosSergio.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EscuelaNegociosSergio.Controllers
{
    public class ClubController : Controller
    {
        private readonly SchoolDbContext _db;

        public ClubController(SchoolDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult> AllClub()
        {
            var clubs = await _db.Clubs.Include(c => c.Department).ToListAsync();
            return View(clubs);
        }

        public async Task<ActionResult> AddClub()
        {
            var departmentDisplay = await _db.Departments.Select(d => new
            {
                Id = d.DepartmentId,
                Value = d.DepartmentName
            }).ToListAsync();

            AddClubViewModel vm = new AddClubViewModel();

            vm.DepartmentList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(departmentDisplay, "Id", "Value");
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddClub(AddClubViewModel vm)
        {
            var department = await _db.Departments.SingleOrDefaultAsync(d => d.DepartmentId == vm.Department.DepartmentId);
            vm.Club.Department = department;
            _db.Add(vm.Club);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllClub");
        }

        public async Task<IActionResult> EditClub(int id)
        {
            AddClubViewModel vm = new AddClubViewModel();

            var departmentDisplay = await _db.Departments.Select(d => new
            {
                Id = d.DepartmentId,
                Value = d.DepartmentName
            }).ToListAsync();

            var actualDepartment = _db.Clubs.Include(c => c.Department).Where(c => c.ClubId == id).Select(c => c.Department.DepartmentId).FirstOrDefault();

            vm.DepartmentList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(departmentDisplay,
                "Id", "Value", actualDepartment);

            Club cd = await _db.Clubs.FindAsync(id);
            vm.Club = cd;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditClub(AddClubViewModel vm)
        {
            var selectedDepartment = vm.Department.DepartmentId;
            var choosenDepartment = await _db.Departments.SingleOrDefaultAsync(dpt => dpt.DepartmentId == selectedDepartment);

            vm.Club.Department = choosenDepartment;

            _db.Update(vm.Club);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllClub");
        }

        public async Task<IActionResult> DeleteClub(int id)
        {
            Club cb;
            cb = await _db.Clubs.Include(c => c.Department).Where(c => c.ClubId == id).FirstOrDefaultAsync();
            return View(cb);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteClub(Club club)
        {
            _db.Remove(club);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllClub");
        }
    }
}
