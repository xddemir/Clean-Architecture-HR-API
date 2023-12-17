using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class UserRoleConfiguration: IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(new IdentityUserRole<string>()
            {
                RoleId = "621d343c-7a85-4765-8623-d6773bce004b",
                UserId = "5de4f0e1-8827-40a8-b234-2ac7fa3f978d",
            },
            new IdentityUserRole<string>()
            {
                RoleId = "84525ef6-82cd-494c-a162-5b60b384b29a",
                UserId = "c75d7fc3-0657-4f63-8951-07a90cd565ab",
            }
        );

    }
}