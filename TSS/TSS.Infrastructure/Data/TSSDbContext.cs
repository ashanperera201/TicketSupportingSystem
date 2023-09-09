using Microsoft.EntityFrameworkCore;
using TSS.Domain.Entities;

namespace TSS.Infrastructure.Data
{
    public class TSSDbContext: DbContext
    {
        public TSSDbContext(DbContextOptions<TSSDbContext> options): base(options)
        {
        }

        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
    }
}
