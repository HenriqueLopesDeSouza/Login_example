using System.ComponentModel.DataAnnotations;

namespace Application.Api.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "The  field {0} is required.")]
        [StringLength(200, ErrorMessage = "The field {0} must be between {2} and  {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The  field {0} is required.")]
        [StringLength(200, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The  field {0} is required.")]
        [StringLength(200, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Password { get; set; }

        [Required(ErrorMessage = "The  field {0} is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(200, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Email { get; set; }

        [Required(ErrorMessage = "The  field {0} is required.")]
        [StringLength(200, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Role { get; set; }
    }
}
