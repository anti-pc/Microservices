using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models
{
    public class SigninInput
    {
        [Required]
        [Display(Name ="Your email address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Your password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Remember Me")]
        public bool IsRemember { get; set; }
    }
}
