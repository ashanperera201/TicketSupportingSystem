using TSS.Application.Core.Models.DTOs;

namespace TSS.Application.Core.Models.Responses
{
    public class UserResponse
    {
        public UserDto User { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string ErrorMessage { get; set; }
    }
}
