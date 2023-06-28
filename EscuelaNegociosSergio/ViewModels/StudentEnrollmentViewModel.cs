using EscuelaNegociosSergio.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EscuelaNegociosSergio.ViewModels
{
    public class StudentEnrollmentViewModel
    {
        public Enrollment Enrollment { get; set; }
        public SelectList StudentList { get; set; }
    }
}
