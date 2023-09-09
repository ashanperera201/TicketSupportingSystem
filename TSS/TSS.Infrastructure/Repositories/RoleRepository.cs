#region References
using Microsoft.EntityFrameworkCore;
using TSS.Domain.Core.Repositories;
using TSS.Domain.Entities;
using TSS.Infrastructure.Data;
#endregion

#region Namespace
namespace TSS.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        /// <summary>
        /// The TSS database context
        /// </summary>
        private readonly TSSDbContext _tssDbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="tssDbContext">The TSS database context.</param>
        public RoleRepository(TSSDbContext tssDbContext)
        {
            _tssDbContext = tssDbContext;
        }

        /// <summary>
        /// Gets the role asynchronous.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Roles?> GetRoleAsync(string roleId, CancellationToken cancellationToken = default)
        {
            var role = await _tssDbContext.Roles.FirstOrDefaultAsync(x => x.RoleId == Guid.Parse(roleId), cancellationToken);
            return role;
        }

        /// <summary>
        /// Saves the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Roles?> SaveRoleAsync(Roles role, CancellationToken cancellationToken = default)
        {
            var transaction = await _tssDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var savedRes = await _tssDbContext.Roles.AddAsync(role, cancellationToken);
                await _tssDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return savedRes.Entity;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        /// <summary>
        /// Updates the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Roles?> UpdateRoleAsync(Roles role, CancellationToken cancellationToken = default)
        {
            var transaction = await _tssDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var savedRes = _tssDbContext.Roles.Update(role);
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
    }
}
#endregion