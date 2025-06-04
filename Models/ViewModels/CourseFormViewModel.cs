using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourseRegistration_FatemaSarah.Models.ViewModels
{
    public class CourseFormViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, 100)]
        public int Capacity { get; set; }

        [Required]
        [Range(1, 100000)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 52)]
        public int Duration { get; set; }

        [Required]
        public int InstructorId { get; set; }

        public IFormFile? PdfFile { get; set; }

        public List<SelectListItem> Instructors { get; set; } = new();
    }
}
