using ClassLibrary1HR.LeaveManagement.Persistance.DatabaseContext;
using ClassLibrary1HR.LeaveManagement.Persistance.Repositories;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClassLibrary1HR.LeaveManagement.Persistance;

public static class PersistanceServiceRegistration
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<HrDatabaseContext>(opt => {
            opt.UseSqlServer(configuration.GetConnectionString("HrDatabaseConnectionString"),
             b => b.MigrationsAssembly(typeof(HrDatabaseContext).Assembly.FullName));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        services.AddScoped<ILeaveAllocationeRepository, LeaveAllocationRepository>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
    
        return services;
    }
}