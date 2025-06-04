using System.ComponentModel.DataAnnotations;

namespace OnlineCourseRegistration_FatemaSarah.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        [MaxLength(50)]
        public string StudentName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Phone]
        public string Phone {  get; set; }
        public string City { get; set; }

        public string? PhotoPath { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    }
}
