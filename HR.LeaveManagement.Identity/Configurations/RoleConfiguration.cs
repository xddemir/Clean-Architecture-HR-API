using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole()
            {
                Id = "621d343c-7a85-4765-8623-d6773bce004b",
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            },
            new IdentityRole()
            {
                Id = "84525ef6-82cd-494c-a162-5b60b384b29a",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            });
    }
}