using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistance;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries;

public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, GetLeaveRequestDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IAppLogger<GetLeaveRequestDetailsQueryHandler> _logger;


    public GetLeaveRequestDetailsQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository, IAppLogger<GetLeaveRequestDetailsQueryHandler> logger)
    {
        _mapper = mapper;
        _leaveRequestRepository = leaveRequestRepository;
        _logger = logger;
    }

    public async Task<GetLeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
    {
        var requestDetail = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);

        var detail = _mapper.Map<GetLeaveRequestDetailsDto>(requestDetail);

        return detail;
    }
}