namespace EscuelaNegociosSergio.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int ClubId { get; set; }
        public Student Student { get; set; }
        public Club Club { get; set; }
    }
}
