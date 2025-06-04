using System.ComponentModel.DataAnnotations;

namespace OnlineCourseRegistration_FatemaSarah.Models
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Major {  get; set; }

    }
}
