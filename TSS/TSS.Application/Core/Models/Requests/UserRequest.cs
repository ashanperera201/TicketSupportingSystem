using System.ComponentModel.DataAnnotations;

namespace TSS.Application.Core.Models.Requests
{
    public class UserRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public string StatusText { get; set; }
        [Required]
        public string Password { get; set; }
        public string? PasswordSalt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? RoleId { get; set; }

    }
}
