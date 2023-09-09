using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TSS.Domain.Core.Repositories;
using TSS.Infrastructure.Data;
using TSS.Infrastructure.Repositories;

namespace TSS.Infrastructure
{
    public static class InfrastructureInjections
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<TSSDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly("TSS.Infrastructure")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
        }

        public static void MigrateDatabase(this IServiceProvider serviceProvider)
        {
            var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<TSSDbContext>>();

            using (var dbContext = new TSSDbContext(dbContextOptions))
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
