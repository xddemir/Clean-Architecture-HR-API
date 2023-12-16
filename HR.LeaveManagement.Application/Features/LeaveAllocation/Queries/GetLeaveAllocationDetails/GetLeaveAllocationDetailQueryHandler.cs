using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailQueryHandler: IRequestHandler<GetLeaveAllocationDetailQuery, LeaveAllocationDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationeRepository _repository;
    private readonly IAppLogger<GetLeaveAllocationDetailQueryHandler> _logger;


    public GetLeaveAllocationDetailQueryHandler(IMapper mapper, ILeaveAllocationeRepository repository, IAppLogger<GetLeaveAllocationDetailQueryHandler> logger)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
    }

    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailQuery request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _repository.GetLeaveAllocationWithDetails(request.Id);
        var allocation = _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation);
        _logger.LoginInformation("Fetched successfully");

        return allocation;

    }
}