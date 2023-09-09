#region References
using TSS.Application.Core.Models.DTOs;
using TSS.Application.Core.Models.Responses;
#endregion

#region Namespace
namespace TSS.Application.Interfaces
{
    public interface ITokenService
    {
        /// <summary>
        /// Generates the token asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public TokenResponse? GenerateTokenAsync(UserDto user);
        /// <summary>
        /// Decodes the user token.
        /// </summary>
        /// <returns></returns>
        public DecodedTokenResponse? DecodeUserToken();
    }
}
#endregion