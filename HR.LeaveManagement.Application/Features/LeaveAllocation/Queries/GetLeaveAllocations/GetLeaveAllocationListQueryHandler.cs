using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocation;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocationListQueryHandler: IRequestHandler<GetLeaveAllocationListQuery, List<LeaveAllocationDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationeRepository _repository;
    private readonly IAppLogger<GetLeaveAllocationListQueryHandler> _logger;

    public GetLeaveAllocationListQueryHandler(IMapper mapper, ILeaveAllocationeRepository repository, IAppLogger<GetLeaveAllocationListQueryHandler> logger)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListQuery request, CancellationToken cancellationToken)
    {
        var leaveAllocations = await _repository.GetLeaveAllocationsWithDetails();
        var allocations = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
        
        _logger.LoginInformation("Fetched successfully");
        
        return allocations;
    }
}