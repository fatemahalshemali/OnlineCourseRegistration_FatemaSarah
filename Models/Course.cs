using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourseRegistration_FatemaSarah.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }

        //FK
        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    }
}
