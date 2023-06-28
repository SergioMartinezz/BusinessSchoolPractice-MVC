using EscuelaNegociosSergio.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EscuelaNegociosSergio.ViewModels
{
    public class AddClubViewModel
    {
        public Club Club { get; set; }
        public Department Department { get; set; }
        public SelectList DepartmentList { get; set; }
    }
}
