#region References
using TSS.Application.Interfaces;
using TSS.Application.Core.Models.DTOs;
using TSS.Application.Core.Models.Requests;
using TSS.Domain.Core.Repositories;
using TSS.Domain.Entities;
#endregion

#region Namespace

namespace TSS.Application.Core.Services
{
    public class RoleService : IRoleService
    {
        /// <summary>
        /// The entity mapper service
        /// </summary>
        private readonly IEntityMapperService _entityMapperService;
        /// <summary>
        /// The role repository
        /// </summary>
        private readonly IRoleRepository _roleRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        /// <param name="roleRepository">The role repository.</param>
        public RoleService(IRoleRepository roleRepository, IEntityMapperService entityMapperService)
        {
            _roleRepository = roleRepository;
            _entityMapperService = entityMapperService;
        }

        /// <summary>
        /// Gets the role asynchronous.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<RoleDto?> GetRoleAsync(string roleId, CancellationToken cancellationToken = default)
        {
            var result = await _roleRepository.GetRoleAsync(roleId, cancellationToken);
            if (result != null)
            {
                return _entityMapperService.Map<Roles, RoleDto>(result);
            }
            return null;
        }

        /// <summary>
        /// Saves the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<RoleDto?> SaveRoleAsync(RoleRequest role, CancellationToken cancellationToken = default)
        {
            var mappedResult = _entityMapperService.Map<RoleRequest, Roles>(role);
            var savedResult = await _roleRepository.SaveRoleAsync(mappedResult, cancellationToken);
            if (savedResult != null)
            {
                return _entityMapperService.Map<Roles, RoleDto>(savedResult);
            }
            return null;
        }

        public Task<RoleDto?> UpdateRoleAsync(RoleDto role, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
#endregion