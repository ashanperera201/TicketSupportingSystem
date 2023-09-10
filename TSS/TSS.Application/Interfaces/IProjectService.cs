#region References
using TSS.Application.Core.Models.DTOs;
using TSS.Application.Core.Models.Requests;
#endregion

#region Namespace

namespace TSS.Application.Interfaces
{
    public interface IProjectService
    {
        /// <summary>
        /// Gets the projects asynchronous.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public List<ProjectDto>? GetAllProjects(string customerId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Filters the project query asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public IEnumerable<ProjectDto>? FilterProjectQueryAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Saves the project asynchronous.
        /// </summary>
        /// <param name="projects">The projects.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<ProjectDto> SaveProjectAsync(ProjectRequest projects, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the project asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="projects">The projects.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<ProjectDto?> UpdateProjectAsync(string projectId, ProjectRequest projects, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the projects by identifier asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<ProjectDto?> GetProjectsByIdAsync(string projectId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Deletes the project asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<bool> DeleteProjectByIdAsync(string projectId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Deletes the project by user identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<bool> DeleteProjectByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    }
}
#endregion