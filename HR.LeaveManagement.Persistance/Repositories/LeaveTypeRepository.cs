using ClassLibrary1HR.LeaveManagement.Persistance.DatabaseContext;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1HR.LeaveManagement.Persistance.Repositories;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
    public LeaveTypeRepository(HrDatabaseContext context) : base(context)
    {
    }

    public async Task<bool> IsLeaveTypeUnique(string name)
    {
        var result = await _context.LeaveTypes.AnyAsync(q => q.Name == name);
        return result;
    }
}