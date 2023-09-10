#region References
using Microsoft.EntityFrameworkCore;
using TSS.Domain.Core.Repositories;
using TSS.Domain.Entities;
using TSS.Domain.Enums;
using TSS.Infrastructure.Data;
#endregion

#region Namespace
namespace TSS.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        /// <summary>
        /// The TSS database context
        /// </summary>
        private readonly TSSDbContext _tssDbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectRepository"/> class.
        /// </summary>
        /// <param name="tssDbContext">The TSS database context.</param>
        public ProjectRepository(TSSDbContext tssDbContext)
        {
            _tssDbContext = tssDbContext;
        }

        /// <summary>
        /// Deletes the project by identifier asynchronous.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<bool> DeleteProjectByIdAsync(Projects project, CancellationToken cancellationToken = default)
        {
            var transaction = await _tssDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                _tssDbContext.Projects.Remove(project);
                await _tssDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return true;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        /// <summary>
        /// Deletes the project by user identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<bool> DeleteProjectByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            var project = await _tssDbContext.Projects.FirstOrDefaultAsync(x => x.ProjectAssignedUser == Guid.Parse(userId), cancellationToken);
            if (project != null)
            {
                return await DeleteProjectByIdAsync(project);
            }
            return false;
        }

        /// <summary>
        /// Filters the project query asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public IQueryable<Projects> FilterProjectQuery(CancellationToken cancellationToken = default)
        {
            return _tssDbContext.Projects
                        .Include(i => i.Tickets)
                        .Include(i => i.User)
                        .Where(x => !x.IsDeleted && x.Status == UserStatus.Active);
        }

        /// <summary>
        /// Gets the projects by identifier asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Projects?> GetProjectsByIdAsync(string projectId, CancellationToken cancellationToken = default)
        {
            var project = await _tssDbContext.Projects.FirstOrDefaultAsync(x => x.Id == Guid.Parse(projectId), cancellationToken);
            return project;
        }

        /// <summary>
        /// Saves the project asynchronous.
        /// </summary>
        /// <param name="projects">The projects.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Projects> SaveProjectAsync(Projects projects, CancellationToken cancellationToken = default)
        {
            var transaction = await _tssDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var savedRes = await _tssDbContext.Projects.AddAsync(projects, cancellationToken);
                await _tssDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return savedRes.Entity;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        /// <summary>
        /// Updates the project asynchronous.
        /// </summary>
        /// <param name="projects">The projects.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Projects> UpdateProjectAsync(Projects projects, CancellationToken cancellationToken = default)
        {
            var transaction = await _tssDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var udpatedRes = _tssDbContext.Projects.Update(projects);
                await _tssDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return udpatedRes.Entity;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
#endregion