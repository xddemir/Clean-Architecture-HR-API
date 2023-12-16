using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public record GetLeaveTypesDetailsQuery(int Id) : IRequest<LeaveTypeDetailDto>;