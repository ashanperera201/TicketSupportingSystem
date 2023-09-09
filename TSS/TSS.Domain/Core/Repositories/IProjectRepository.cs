#region References
using TSS.Domain.Entities;
#endregion
namespace TSS.Domain.Core.Repositories
{
    public interface IProjectRepository
    {
        /// <summary>
        /// Filters the project query asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public IQueryable<Projects> FilterProjectQueryAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Saves the project asynchronous.
        /// </summary>
        /// <param name="projects">The projects.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<Projects> SaveProjectAsync(Projects projects, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the project asynchronous.
        /// </summary>
        /// <param name="projects">The projects.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<Projects> UpdateProjectAsync(Projects projects, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the projects by identifier asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<Projects?> GetProjectsByIdAsync(string projectId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Deletes the project asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<bool> DeleteProjectByIdAsync(Projects projects, CancellationToken cancellationToken = default);
        /// <summary>
        /// Deletes the project by user identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<bool> DeleteProjectByUserIdAsync(string userId, CancellationToken cancellationToken = default);

    }
}
