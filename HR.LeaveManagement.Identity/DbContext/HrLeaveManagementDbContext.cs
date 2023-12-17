using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Identity.DbContext;

public class HrLeaveManagementDbContext : IdentityDbContext<ApplicationUser>
{
    public HrLeaveManagementDbContext()
    {
        
    }

    public HrLeaveManagementDbContext(DbContextOptions<HrLeaveManagementDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(HrLeaveManagementDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=db_hr_leavemanagement; User=sa; Password=Password12!;TrustServerCertificate=true");
        
        base.OnConfiguring(optionsBuilder);
    }
}