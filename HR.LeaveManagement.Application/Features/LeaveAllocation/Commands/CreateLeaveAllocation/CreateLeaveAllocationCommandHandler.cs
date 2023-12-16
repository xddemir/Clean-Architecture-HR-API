using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistance;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler: IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationeRepository _repository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<CreateLeaveAllocationCommandHandler> _logger;

    public CreateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationeRepository repository, IAppLogger<CreateLeaveAllocationCommandHandler> logger, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationValidator(_leaveTypeRepository);
        var validations = await validator.ValidateAsync(request, cancellationToken);

        if (validations.Errors.Any())
            _logger.LogWarning("Leave Allocation not failed to create", validations);

        var leavetype = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);


        var leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request);
        await _repository.CreateAsync(leaveAllocation);
        
        return Unit.Value;;
    }
}