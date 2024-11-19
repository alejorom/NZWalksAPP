using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class AuthenticationDbContext : IdentityDbContext
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var readerRoleId = "2c3e1d95-12fc-499e-917d-04a0ccd2bc11";
            var writerRoleId = "a31eeea5-1bc6-4e61-8dfa-c993c215b20d";

            // Seed data for Roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "ADMIN"
                }
            };

            // Seed roles to the database
            modelBuilder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
