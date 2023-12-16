using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries;

public record GetLeaveRequestDetailsQuery(int Id) : IRequest<GetLeaveRequestDetailsDto>;
