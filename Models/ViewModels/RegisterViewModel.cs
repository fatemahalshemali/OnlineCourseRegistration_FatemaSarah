using System.ComponentModel.DataAnnotations;

namespace OnlineCourseRegistration_FatemaSarah.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter Email Address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        public string Mobile { get; set; }

        public string City { get; set; }
    }
}
