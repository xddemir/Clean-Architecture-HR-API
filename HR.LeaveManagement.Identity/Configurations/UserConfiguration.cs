using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        builder.HasData(new ApplicationUser()
            {
                Id = "5de4f0e1-8827-40a8-b234-2ac7fa3f978d",
                Email = "admin@test.com",
                NormalizedEmail = "ADMIN@TEST.COM",
                FirstName = "System",
                LastName = "Admin",
                UserName = "admin@test.com",
                NormalizedUserName = "ADMIN@TEST.COM",
                PasswordHash = hasher.HashPassword(null, "Password12!"),
                EmailConfirmed = true
            },
            new ApplicationUser()
            {
                Id = "c75d7fc3-0657-4f63-8951-07a90cd565ab",
                Email = "user@test.com",
                NormalizedEmail = "USER@TEST.COM",
                FirstName = "System",
                LastName = "User",
                UserName = "user@test.com",
                NormalizedUserName = "USER@TEST.COM",
                PasswordHash = hasher.HashPassword(null, "Password12!"),
                EmailConfirmed = true
            }
        );
    }
}