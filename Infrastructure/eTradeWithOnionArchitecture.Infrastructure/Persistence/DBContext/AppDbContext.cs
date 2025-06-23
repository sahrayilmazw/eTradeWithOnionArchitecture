using eTradeWithOnionArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics; // Don't forget to add this for ModelBuilder

namespace eTradeWithOnionArchitecture.Infrastructure.Persistence.DBContext
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // IMPORTANT: Call the base method for Identity

            // Create a Password Hasher
            var hasher = new PasswordHasher<AppUser>();

            // --- Seed Users ---
            var adminUser = new AppUser
            {
                Id = "a18be9c0-aa65-4af8-bd17-002000200001", // Unique ID for the user
                Fullname = "Admin User", // Added Fullname
                UserName = "admin@etrade.com",
                NormalizedUserName = "ADMIN@ETRADE.COM",
                Email = "admin@etrade.com",
                NormalizedEmail = "ADMIN@ETRADE.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"), // Generate a new security stamp
                ConcurrencyStamp = Guid.NewGuid().ToString("D") // Important for concurrency control in Identity
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin123."); // Hash the password

            var regularUser = new AppUser
            {
                Id = "b18be9c0-aa65-4af8-bd17-002000200002", // Unique ID for the user
                Fullname = "Regular User", // Added Fullname
                UserName = "user@etrade.com",
                NormalizedUserName = "USER@ETRADE.COM",
                Email = "user@etrade.com",
                NormalizedEmail = "USER@ETRADE.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D") // Important for concurrency control in Identity
            };
            regularUser.PasswordHash = hasher.HashPassword(regularUser, "User123."); // Hash the password

            var editorUser = new AppUser
            {
                Id = "e18be9c0-aa65-4af8-bd17-002000200005", // Another unique ID
                Fullname = "Editor Contributor", // Added Fullname
                UserName = "editor@etrade.com",
                NormalizedUserName = "EDITOR@ETRADE.COM",
                Email = "editor@etrade.com",
                NormalizedEmail = "EDITOR@ETRADE.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D")
            };
            editorUser.PasswordHash = hasher.HashPassword(editorUser, "Editor123.");


            builder.Entity<AppUser>().HasData(adminUser, regularUser, editorUser);

            // --- Seed Roles ---
            var adminRole = new IdentityRole
            {
                Id = "c18be9c0-aa65-4af8-bd17-002000200003", // Unique ID for the role
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString("D") // Important for concurrency control in Identity
            };
            var userRole = new IdentityRole
            {
                Id = "d18be9c0-aa65-4af8-bd17-002000200004", // Unique ID for the role
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = Guid.NewGuid().ToString("D")
            };
            var editorRole = new IdentityRole
            {
                Id = "f18be9c0-aa65-4af8-bd17-002000200006", // Another unique ID for the role
                Name = "Editor",
                NormalizedName = "EDITOR",
                ConcurrencyStamp = Guid.NewGuid().ToString("D")
            };
            builder.Entity<IdentityRole>().HasData(adminRole, userRole, editorRole);

            // --- Assign Roles to Users (IdentityUserRole) ---
            builder.Entity<IdentityUserRole<string>>().HasData(
                // Admin User gets Admin Role
                new IdentityUserRole<string>
                {
                    UserId = adminUser.Id,
                    RoleId = adminRole.Id
                },
                // Regular User gets User Role
                new IdentityUserRole<string>
                {
                    UserId = regularUser.Id,
                    RoleId = userRole.Id
                },
                // Editor User gets Editor Role
                new IdentityUserRole<string>
                {
                    UserId = editorUser.Id,
                    RoleId = editorRole.Id
                }
            );
        }
    }
}