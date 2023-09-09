#region References
using System.ComponentModel.DataAnnotations;
#endregion

#region Namespace
namespace TSS.Application.Core.Models.Requests
{
    public class AuthRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
#endregion