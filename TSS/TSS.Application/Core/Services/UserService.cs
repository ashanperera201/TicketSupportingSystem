#region References
using TSS.Application.Interfaces;
using TSS.Application.Core.Models.DTOs;
using TSS.Application.Core.Models.Requests;
using TSS.Application.Core.Models.Responses;
using TSS.Application.Utils;
using TSS.Domain.Core.Repositories;
using TSS.Domain.Entities;
using TSS.Domain.Enums;
#endregion

namespace TSS.Application.Core.Services
{
    public class UserService : IUserService
    {
        /// <summary>
        /// The token service
        /// </summary>
        private readonly ITokenService _tokenService;
        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// The entity mapper service
        /// </summary>
        private readonly IEntityMapperService _entityMapperService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="entityMapperService">The entity mapper service.</param>
        public UserService(
            IUserRepository userRepository,
            IEntityMapperService entityMapperService,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _entityMapperService = entityMapperService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Authenticates the user asynchronous.
        /// </summary>
        /// <param name="auth">The authentication.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<UserAuthResponse> AuthenticateUserAsync(AuthRequest auth, CancellationToken cancellationToken = default)
        {
            var response = new UserAuthResponse();
            var user = await _userRepository.GetUserByEmailAsync(auth.UserName, cancellationToken);
            if (user != null)
            {
                var isValidPassword = CryptographyProcessor.AreEqual(auth.Password, user.Password, user.PasswordSalt);
                if (isValidPassword)
                {
                    var tokenResult = _tokenService.GenerateTokenAsync(_entityMapperService.Map<Users, UserDto>(user));
                    if (tokenResult != null)
                    {
                        response.IsSuccess = true;
                        response.AccessToken = tokenResult.AccessToken;
                    }
                    return response;
                }
                else
                {
                    return response;
                }
            }
            else
            {
                return response;
            }
        }

        /// <summary>
        /// Gets the user by email asynchronous.
        /// </summary>
        /// <param name="userEmail">The user email.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<UserResponse?> GetUserByEmailAsync(string userEmail, CancellationToken cancellationToken = default)
        {
            var userResult = await _userRepository.GetUserByEmailAsync(userEmail, cancellationToken);
            if (userResult != null)
            {
                return _entityMapperService.Map<Users, UserResponse>(userResult);
            }
            return null;
        }

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<UserResponse?> GetUserById(string userId, CancellationToken cancellationToken = default)
        {
            var userResult = await _userRepository.GetUserById(userId, cancellationToken);
            if (userResult != null)
            {
                return _entityMapperService.Map<Users, UserResponse>(userResult);
            }
            return null;
        }

        /// <summary>
        /// Saves the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<UserResponse?> SaveUserAsync(UserRequest user, CancellationToken cancellationToken = default)
        {
            var response = new UserResponse();
            var existsUser = await _userRepository.GetUserByEmailAsync(user.EmailId, cancellationToken);

            if (existsUser != null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "User is already exists.";
                return response;
            }


            string passwordSalt = CryptographyProcessor.CreateSalt(300);
            user.PasswordSalt = passwordSalt;
            user.Password = CryptographyProcessor.GenerateHash(user.Password, passwordSalt);


            var mappedResult = _entityMapperService.Map<UserRequest, Users>(user);
            var savedUser = await _userRepository.SaveUserAsync(mappedResult, cancellationToken);
            if (savedUser != null)
            {
                return _entityMapperService.Map<Users, UserResponse>(savedUser);
            }
            return response;
        }

        /// <summary>
        /// Updates the user asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<UserResponse?> UpdateUserAsync(UserUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var response = new UserResponse();
            var user = await _userRepository.GetUserByEmailAsync(request.EmailId, cancellationToken);

            if (user != null)
            {
                user.Status = (UserStatus)request.Status;
                user.IsDeleted = request.IsDeleted;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.LastModifiedOn = DateTime.UtcNow;

                if (!string.IsNullOrEmpty(request.RoleId))
                {
                    user.RoleId = Guid.Parse(request.RoleId.ToString());
                }

                var updatedUser = await _userRepository.UpdateUserAsync(user, cancellationToken);
                if (updatedUser != null)
                {
                    return _entityMapperService.Map<Users, UserResponse>(updatedUser);
                }
            }
            return response;
        }
    }
}
