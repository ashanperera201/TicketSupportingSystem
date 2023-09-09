#region References
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using TSS.Application.Interfaces;
using TSS.Application.Core.Models.DTOs;
using TSS.Application.Core.Models.Requests;
using TSS.Domain.Entities;
#endregion

#region Namespace
namespace TSS.API.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    public class UsersController : Controller
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService _userService;
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Authenticates the user asynchronous.
        /// </summary>
        /// <param name="auth">The authentication.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("auth/login")]
        public async Task<IActionResult> AuthenticateUserAsync([FromBody] AuthRequest auth, CancellationToken cancellationToken)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.AuthenticateUserAsync(auth, cancellationToken);
                    return StatusCode(result.IsSuccess ? StatusCodes.Status200OK : StatusCodes.Status401Unauthorized, result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, JsonConvert.SerializeObject(ModelState.Values.Select(x => x.Errors)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Registers the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("auth/register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRequest user, CancellationToken cancellationToken)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.SaveUserAsync(user, cancellationToken);
                    return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError, result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, JsonConvert.SerializeObject(ModelState.Values.Select(x => x.Errors)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the user by email asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>

        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    var user = await _userService.GetUserByEmailAsync(email, cancellationToken);
                    return StatusCode(user != null ? StatusCodes.Status200OK : StatusCodes.Status204NoContent, user);
                }
                else
                {
                    // ERRORS SHOULD BE GET FROM THE RESOURCE FILES
                    return StatusCode(StatusCodes.Status400BadRequest, "User email is required.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the user by identifier asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var user = await _userService.GetUserById(id, cancellationToken);
                    return StatusCode(user != null ? StatusCodes.Status200OK : StatusCodes.Status204NoContent, user);
                }
                else
                {
                    // ERRORS SHOULD BE GET FROM THE RESOURCE FILES
                    return StatusCode(StatusCodes.Status400BadRequest, "User email is required.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserUpdateRequest user, CancellationToken cancellationToken)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var updatedRes = await _userService.UpdateUserAsync(user, cancellationToken);
                    return StatusCode(updatedRes != null ? StatusCodes.Status200OK : StatusCodes.Status204NoContent, updatedRes);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, JsonConvert.SerializeObject(ModelState.Values.Select(x => x.Errors)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
#endregion