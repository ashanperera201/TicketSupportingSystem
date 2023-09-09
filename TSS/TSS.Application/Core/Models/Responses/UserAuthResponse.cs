namespace TSS.Application.Core.Models.Responses
{
    public class UserAuthResponse
    {
        public string AccessToken { get; set; }
        public bool IsSuccess { get; set; } = false;
    }
}
