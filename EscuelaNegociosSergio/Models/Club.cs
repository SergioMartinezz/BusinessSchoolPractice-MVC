namespace EscuelaNegociosSergio.Models
{
    public class Club
    {
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public int StudentsNumber { get; set;}
        public Department Department { get; set; }
    }
}
