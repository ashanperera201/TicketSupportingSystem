using Microsoft.Extensions.DependencyInjection;
using TSS.Application.Interfaces;
using TSS.Application.Core.Services;

namespace TSS.Application
{
    public static class ApplicationDependencyInjections
    {
        public static void ConfigureApplicationInjections(this IServiceCollection services)
        {
            services.AddScoped<IEntityMapperService, EntityMapperService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IProjectService, ProjectService>();
        }
    }
}
