using Microsoft.AspNetCore.Identity;

namespace OnlineCourseRegistration_FatemaSarah.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? City { get; set; }
        public string? Gender { get; set; }


    }
}
