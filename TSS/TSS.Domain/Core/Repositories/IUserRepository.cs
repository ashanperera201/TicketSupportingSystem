using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSS.Domain.Entities;

namespace TSS.Domain.Core.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Saves the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<Users?> SaveUserAsync(Users user, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the user by email asynchronous.
        /// </summary>
        /// <param name="userEmail">The user email.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<Users?> GetUserByEmailAsync(string userEmail, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<Users?> UpdateUserAsync(Users user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<Users?> GetUserById(string userId, CancellationToken cancellationToken = default);
    }
}
