using System.ComponentModel.DataAnnotations;

namespace OnlineCourseRegistration_FatemaSarah.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
