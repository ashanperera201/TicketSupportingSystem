using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TSS.Application.Core.Models.Requests;
using TSS.Application.Interfaces;

namespace TSS.API.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/projects")]
    [ApiController]
    public class ProjectsController : Controller
    {
        /// <summary>
        /// The project service
        /// </summary>
        private readonly IProjectService _projectService;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController"/> class.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Gets all projects.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllProjects()
        {
            try
            {
                var projects = _projectService.GetAllProjects(string.Empty);
                return StatusCode(projects != null ? StatusCodes.Status200OK : StatusCodes.Status417ExpectationFailed, projects);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the project by identifier asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet("id/{projectId}")]
        public async Task<IActionResult> GetProjectByIdAsync(string projectId, CancellationToken cancellationToken)
        {
            try
            {
                if (!string.IsNullOrEmpty(projectId))
                {
                    var project = await _projectService.GetProjectsByIdAsync(projectId, cancellationToken);
                    return StatusCode(project != null ? StatusCodes.Status200OK : StatusCodes.Status417ExpectationFailed, project);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Saves the project asynchronous.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> SaveProjectAsync([FromBody] ProjectRequest project, CancellationToken cancellationToken)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _projectService.SaveProjectAsync(project, cancellationToken);
                    return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, result);
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
        /// Updates the project asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="project">The project.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPut("{projectId}")]
        public async Task<IActionResult> UpdateProjectAsync(string projectId, [FromBody] ProjectRequest project, CancellationToken cancellationToken)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _projectService.UpdateProjectAsync(projectId, project, cancellationToken);
                    return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, result);
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
        /// Deletes the project by identifier asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpDelete("id/{projectId}")]
        public async Task<IActionResult> DeleteProjectByIdAsync(string projectId, CancellationToken cancellationToken)
        {
            try
            {
                if (!string.IsNullOrEmpty(projectId))
                {
                    var project = await _projectService.DeleteProjectByIdAsync(projectId, cancellationToken);
                    return StatusCode(project ? StatusCodes.Status200OK : StatusCodes.Status417ExpectationFailed, project);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deletes the project by user identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpDelete("by-user/{userId}")]
        public async Task<IActionResult> DeleteProjectByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    var project = await _projectService.DeleteProjectByUserIdAsync(userId, cancellationToken);
                    return StatusCode(project ? StatusCodes.Status200OK : StatusCodes.Status417ExpectationFailed, project);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
