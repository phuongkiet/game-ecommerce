using BusinessObject;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class GameEcommerceContext : IdentityDbContext<User,
                                                        Role,
                                                        int,
                                                        IdentityUserClaim<int>,
                                                        UserRole,
                                                        IdentityUserLogin<int>,
                                                        IdentityRoleClaim<int>,
                                                        IdentityUserToken<int>
                                                        >
    {
        public GameEcommerceContext() { }
        public GameEcommerceContext(DbContextOptions options) : base(options) { }
        public DbSet<User>? Users { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<UserRole>? UserRoles { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(GetConnectionString());
        }

        private string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            return "Data Source=(local);database=GameEcommerce;uid=sa;pwd=12345;TrustServerCertificate=True;MultipleActiveResultSets=True";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>()
            .HasMany(e => e.UserRoles)
            .WithOne(e => e.Role)
            .HasForeignKey(e => e.RoleId)
            .IsRequired();

            modelBuilder.Entity<User>()
            .HasMany(e => e.UserRoles)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        }
    }
}
