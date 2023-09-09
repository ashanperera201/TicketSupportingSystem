using System.ComponentModel.DataAnnotations;

namespace TSS.Application.Core.Models.Requests
{
    public class UserUpdateRequest
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
        public bool IsDeleted { get; set; } = false;
        public string? RoleId { get; set; }
    }
}
