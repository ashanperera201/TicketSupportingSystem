#region References
using TSS.Application.Core.Models.Requests;
using TSS.Application.Core.Models.Responses;
#endregion

#region Namespace

namespace TSS.Application.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Saves the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<UserResponse?> SaveUserAsync(UserRequest user, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the user asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<UserResponse?> UpdateUserAsync(UserUpdateRequest request, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the user by email asynchronous.
        /// </summary>
        /// <param name="userEmail">The user email.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<UserResponse?> GetUserByEmailAsync(string userEmail, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<UserResponse?> GetUserById(string userId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Authenticates the user asynchronous.
        /// </summary>
        /// <param name="auth">The authentication.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<UserAuthResponse> AuthenticateUserAsync(AuthRequest auth, CancellationToken cancellationToken = default);
    }
}
#endregion