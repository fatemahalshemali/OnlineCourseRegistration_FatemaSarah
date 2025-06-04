using System.ComponentModel.DataAnnotations;

namespace OnlineCourseRegistration_FatemaSarah.Models.ViewModels
{
    public class EditRoleViewModel
    {
        public string RoleId { get; set; }
        [Required(ErrorMessage = "Enter Role Name")]
        [MinLength(2)]
        public string RoleName { get; set; }

    }
}
