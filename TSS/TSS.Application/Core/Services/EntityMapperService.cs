#region References
using AutoMapper;
using Microsoft.AspNetCore.Http;
using TSS.Application.Interfaces;
using TSS.Application.Core.Models.DTOs;
using TSS.Application.Core.Models.Requests;
using TSS.Application.Core.Models.Responses;
using TSS.Domain.Entities;
#endregion

#region Namespace
namespace TSS.Application.Core.Services
{
    public class EntityMapperService : IEntityMapperService
    {
        /// <summary>
        /// The token service
        /// </summary>
        private readonly ITokenService _tokenService;
        /// <summary>
        /// The mapper configuration
        /// </summary>
        private MapperConfiguration _mapperConfiguration;
        /// <summary>
        /// The mapper
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityMapperService" /> class.
        /// </summary>
        /// <param name="tokenService">The token service.</param>
        public EntityMapperService(ITokenService tokenService)
        {
            _tokenService = tokenService;

            _configureMapper();
            _createMapper();
        }




        /// <summary>
        /// Configures the mapper.
        /// </summary>
        private void _configureMapper()
        {
            _mapperConfiguration = new MapperConfiguration(mapConfig =>
            {

                #region User Login
                mapConfig.CreateMap<UserRequest, Users>()
                       .BeforeMap((s, d) => d.CreatedOn = DateTime.UtcNow)
                       .ForMember(x => x.RoleId, m => m.MapFrom(t => t.RoleId))
                       .ForMember(x => x.FirstName, m => m.MapFrom(t => t.FirstName))
                       .ForMember(x => x.LastName, m => m.MapFrom(t => t.LastName))
                       .ForMember(x => x.EmailId, m => m.MapFrom(t => t.EmailId))
                       .ForMember(x => x.Password, m => m.MapFrom(t => t.Password))
                       .ForMember(x => x.PasswordSalt, m => m.MapFrom(t => t.PasswordSalt))
                       .ForMember(x => x.Status, m => m.MapFrom(t => t.Status))
                       .ReverseMap();

                mapConfig.CreateMap<UserDto, Users>()
                       .ForMember(x => x.FirstName, m => m.MapFrom(t => t.FirstName))
                       .ForMember(x => x.LastName, m => m.MapFrom(t => t.LastName))
                       .ForMember(x => x.EmailId, m => m.MapFrom(t => t.EmailId))
                       .ForMember(x => x.Status, m => m.MapFrom(t => t.Status))
                       .ForMember(x => x.Projects, m => m.MapFrom(t => t.Projects))
                       .ReverseMap();

                mapConfig.CreateMap<UserResponse, Users>()
                       .BeforeMap((s, d) => s.IsSuccess = true)
                       .BeforeMap((s, d) => s.ErrorMessage = String.Empty)
                       .ForMember(x => x.EmailId, m => m.MapFrom(t => t.User.EmailId))
                       .ForMember(x => x.FirstName, m => m.MapFrom(t => t.User.FirstName))
                       .ForMember(x => x.LastName, m => m.MapFrom(t => t.User.LastName))
                       .ForMember(x => x.Status, m => m.MapFrom(t => t.User.Status))
                       .ForMember(x => x.Id, m => m.MapFrom(t => t.User.Id))
                       .ForMember(x => x.Role, m => m.MapFrom(t => t.User.Role))
                       .ReverseMap();
                #endregion

                #region Roles
                mapConfig.CreateMap<RoleDto, Roles>()
                       .BeforeMap((s, d) => s.CreatedOn = DateTime.UtcNow)
                       .ForMember(x => x.RoleId, m => m.MapFrom(t => t.Id))
                       .ForMember(x => x.Code, m => m.MapFrom(t => t.RoleCode))
                       .ForMember(x => x.Name, m => m.MapFrom(t => t.RoleName))
                       .ForMember(x => x.Status, m => m.MapFrom(t => t.Status))
                       .ForMember(x => x.IsDeleted, m => m.MapFrom(t => t.IsDeleted))
                       .ReverseMap();

                mapConfig.CreateMap<RoleRequest, Roles>()
                     .BeforeMap((s, d) => d.CreatedOn = DateTime.UtcNow)
                     .BeforeMap((s, d) => d.CreatedBy = Guid.Parse(_tokenService.DecodeUserToken()?.UserId.ToString()!))
                     .ForMember(x => x.Code, m => m.MapFrom(t => t.RoleCode))
                     .ForMember(x => x.Name, m => m.MapFrom(t => t.RoleName))
                     .ForMember(x => x.Status, m => m.MapFrom(t => t.Status))
                     .ForMember(x => x.IsDeleted, m => m.MapFrom(t => t.IsDeleted))
                     .ReverseMap();
                #endregion

                #region Projects Mapping
                mapConfig.CreateMap<ProjectDto, Projects>()
                     .ForMember(x => x.Tickets, m => m.MapFrom(t => t.Tickets))
                     .ReverseMap();
                #endregion

                #region Tickets Mapping
                mapConfig.CreateMap<TicketsDto, Tickets>()
                     .ReverseMap();
                #endregion



            });
        }


        /// <summary>
        /// Creates the mapper.
        /// </summary>
        private void _createMapper()
        {
            _mapper = _mapperConfiguration.CreateMapper();
        }


        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
    }
}
#endregion