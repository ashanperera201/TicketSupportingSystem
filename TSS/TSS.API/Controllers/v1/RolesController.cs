#region References
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TSS.Application.Interfaces;
using TSS.Application.Core.Models.Requests;
#endregion

#region Namespace
namespace TSS.API.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/roles")]
    [ApiController]
    public class RolesController : Controller
    {
        /// <summary>
        /// The role service
        /// </summary>
        private readonly IRoleService _roleService;
        /// <summary>
        /// Initializes a new instance of the <see cref="RolesController"/> class.
        /// </summary>
        /// <param name="roleService">The role service.</param>
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Gets the role asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleAsync(string id, CancellationToken cancellationToken = default)
        {
            try
            {
                var role = await _roleService.GetRoleAsync(id, cancellationToken);
                return StatusCode(role != null ? StatusCodes.Status200OK : StatusCodes.Status417ExpectationFailed, role);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Saves the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveRoleAsync([FromBody] RoleRequest role, CancellationToken cancellationToken)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var savedRole = await _roleService.SaveRoleAsync(role, cancellationToken);
                    return StatusCode(savedRole != null ? StatusCodes.Status200OK : StatusCodes.Status417ExpectationFailed, savedRole);
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