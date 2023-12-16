using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocation;

public record GetLeaveAllocationListQuery : IRequest<List<LeaveAllocationDto>>;
