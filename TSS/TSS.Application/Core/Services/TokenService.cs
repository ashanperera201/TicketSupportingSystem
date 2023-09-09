#region References
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TSS.Application.Interfaces;
using TSS.Application.Core.Models.DTOs;
using TSS.Application.Core.Models.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
#endregion

#region Namespace
namespace TSS.Application.Core.Services
{
    public class TokenService : ITokenService
    {
        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public TokenService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Decodes the user token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public DecodedTokenResponse? DecodeUserToken()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenResult = tokenHandler.ReadJwtToken(token);

                if (tokenResult != null)
                {
                    var userId = tokenResult.Claims.FirstOrDefault(x => x.Type == "Id")?.Value?.ToString();
                    var userEmail = tokenResult.Claims.FirstOrDefault(x => x.Type == "Email")?.Value?.ToString();

                    if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userEmail))
                    {
                        return new DecodedTokenResponse
                        {
                            UserId = userId,
                            UserEmail = userEmail,
                        };
                    }
                    return null;
                }
                return null;
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// Generates the token asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public TokenResponse? GenerateTokenAsync(UserDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWT:Secret").Value!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", user.EmailId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                Issuer = _configuration.GetSection("JWT:ValidIssuer").Value!,
                Audience = _configuration.GetSection("JWT:ValidAudience").Value!,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);

            if (!string.IsNullOrEmpty(stringToken))
            {
                return new TokenResponse
                {
                    AccessToken = stringToken
                };
            }
            return null;
        }
    }
}
#endregion