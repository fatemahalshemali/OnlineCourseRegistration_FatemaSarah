using System.ComponentModel.DataAnnotations;

namespace OnlineCourseRegistration_FatemaSarah.Models.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Enter Role Name")]
        [MinLength(2)]
        public string RoleName { get; set; }


    }
}
