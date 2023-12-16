using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocation;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommand: IRequest<Unit>
{
    public int LeaveTypeId { get; set; }
}