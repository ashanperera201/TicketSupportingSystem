#region References
using TSS.Application.Core.Models.DTOs;
using TSS.Application.Core.Models.Requests;
using TSS.Application.Interfaces;
using TSS.Domain.Core.Repositories;
using TSS.Domain.Entities;
#endregion

#region Namespace

namespace TSS.Application.Core.Services
{
    public class ProjectService : IProjectService
    {
        /// <summary>
        /// The token service
        /// </summary>
        private readonly ITokenService _tokenService;
        /// <summary>
        /// The entity mapper service
        /// </summary>
        private readonly IEntityMapperService _entityMapperService;
        /// <summary>
        /// The project repository
        /// </summary>
        private readonly IProjectRepository _projectRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectService" /> class.
        /// </summary>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="entityMapperService">The entity mapper service.</param>
        public ProjectService(IProjectRepository projectRepository, IEntityMapperService entityMapperService, ITokenService tokenService)
        {
            _projectRepository = projectRepository;
            _entityMapperService = entityMapperService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Deletes the project asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<bool> DeleteProjectByIdAsync(string projectId, CancellationToken cancellationToken = default)
        {
            var project = await _projectRepository.GetProjectsByIdAsync(projectId, cancellationToken);
            if (project != null)
            {
                var deletedRes = await _projectRepository.DeleteProjectByIdAsync(project, cancellationToken);
                return deletedRes;
            }
            return false;
        }

        public async Task<bool> DeleteProjectByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await _projectRepository.DeleteProjectByUserIdAsync(userId, cancellationToken);
        }

        public IEnumerable<ProjectDto>? FilterProjectQueryAsync(CancellationToken cancellationToken = default)
        {
            var query = _projectRepository.FilterProjectQuery(cancellationToken);
            return null;
        }

        /// <summary>
        /// Gets the projects asynchronous.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public List<ProjectDto>? GetAllProjects(string customerId, CancellationToken cancellationToken = default)
        {
            // TODO: MAP CUSTOMER ID LATER.
            // TODO: IF USER IS ADMIN CHANGE THE LOGIC.
            var user = _tokenService.DecodeUserToken()?.UserId?.ToString()!;
            var projectsQuery = _projectRepository.FilterProjectQuery(cancellationToken);
            var projects = projectsQuery.Where(x => x.ProjectAssignedUser == Guid.Parse(user)).ToList();
            return _entityMapperService.Map<List<Projects>, List<ProjectDto>>(projects);
        }

        public async Task<ProjectDto?> GetProjectsByIdAsync(string projectId, CancellationToken cancellationToken = default)
        {
            var project = await _projectRepository.GetProjectsByIdAsync(projectId, cancellationToken);
            if (project != null)
            {
                return _entityMapperService.Map<Projects, ProjectDto>(project);
            }
            return null;
        }

        public async Task<ProjectDto> SaveProjectAsync(ProjectRequest projects, CancellationToken cancellationToken = default)
        {
            var mappedResult = _entityMapperService.Map<ProjectRequest, Projects>(projects);
            var savedResult = await _projectRepository.SaveProjectAsync(mappedResult, cancellationToken);
            return _entityMapperService.Map<Projects, ProjectDto>(savedResult);
        }

        public async Task<ProjectDto?> UpdateProjectAsync(string projectId, ProjectRequest projects, CancellationToken cancellationToken = default)
        {
            var project = await _projectRepository.GetProjectsByIdAsync(projectId, cancellationToken);
            if (project != null)
            {
                project.ProjectName = projects.ProjectName;
                project.ProjectCode = projects.ProjectCode;
                var updatedRes = await _projectRepository.UpdateProjectAsync(project, cancellationToken);
                _entityMapperService.Map<Projects, ProjectDto>(updatedRes);
            }
            return null;
        }
    }
}
#endregion