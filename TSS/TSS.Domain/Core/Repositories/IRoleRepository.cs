#region References
using TSS.Domain.Entities;
#endregion

#region Namespace
namespace TSS.Domain.Core.Repositories
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Saves the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<Roles?> SaveRoleAsync(Roles role, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the role asynchronous.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<Roles?> GetRoleAsync(string roleId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<Roles?> UpdateRoleAsync(Roles role, CancellationToken cancellationToken = default);
    }
}
#endregion