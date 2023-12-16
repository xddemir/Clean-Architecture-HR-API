using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistance;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<GetLeaveRequestListDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IAppLogger<GetLeaveRequestListQueryHandler> _logger;

    public GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequestRepository, IAppLogger<GetLeaveRequestListQueryHandler> logger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _logger = logger;
    }

    public async Task<List<GetLeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
    {
        var requests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
        var leaveRequests = _mapper.Map<List<GetLeaveRequestListDto>>(requests);

        return leaveRequests;

    }
}