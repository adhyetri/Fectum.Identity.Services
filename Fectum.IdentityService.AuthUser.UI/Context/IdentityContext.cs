using Fectum.IdentityService.Model.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Fectum.IdentityService.AuthUser.UI.Context
{
    public class IdentityContext(DbContextOptions<IdentityContext> dbContext, IConfiguration Configuration) : DbContext(dbContext)
    {
        public DbSet<UserRole> IUsersInfoRole { get; set; }
        public IConfiguration Configuration { get; private set; } = Configuration;
        public DbSet<UserInformation> IUsersInformation { get; set; }
        public DbSet<EducationDetails> IUsersInfoEducation { get; set; }
        public DbSet<UserAddressDetails> IUsersInfoAddress { get; set; }
        public DbSet<Registration> IUsersInfoRegistrations { get; set; }
        public DbSet<TechnologyDetails> IUserInfoTechnologyDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInformation>().HasOne(x => x.Registration)
                .WithOne(x => x.UserInformation).HasForeignKey<UserInformation>(x => x.RegistrationId);

            /*modelBuilder.Entity<Registration>().HasOne(x => x.UserRole)
                .WithMany().HasForeignKey(x => x.RoleId);*/

            modelBuilder.Entity<UserAddressDetails>().HasOne(x => x.UserInformation)
                .WithMany(x => x.UserAddressDetails).HasForeignKey(x => x.UserId);

            modelBuilder.Entity<EducationDetails>().HasOne(x => x.UserInformation)
                .WithMany(x => x.EducationDetails).HasForeignKey(x => x.UserId);

            modelBuilder.Entity<TechnologyDetails>().HasOne(x => x.UserInformation)
                .WithMany(x => x.TechnologyDetails).HasForeignKey(x => x.UserId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conn = Configuration.GetConnectionString("connectionString")!;
            if (!string.IsNullOrEmpty(conn))
            {
                optionsBuilder.UseSqlServer(conn);
            }
        }
    }
}
