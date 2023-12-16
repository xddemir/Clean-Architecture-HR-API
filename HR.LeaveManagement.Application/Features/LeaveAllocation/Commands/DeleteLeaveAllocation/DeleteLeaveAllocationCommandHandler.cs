using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler: IRequestHandler<DeleteLeaveAllocationCommand>
{   
    private readonly ILeaveAllocationeRepository _leaveAllocationeRepository;
    private readonly IAppLogger<DeleteLeaveAllocationCommandHandler> _logger;

    public DeleteLeaveAllocationCommandHandler(ILeaveAllocationeRepository leaveAllocationeRepository, IAppLogger<DeleteLeaveAllocationCommandHandler> logger)
    {
        _leaveAllocationeRepository = leaveAllocationeRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationeRepository.GetByIdAsync(request.Id);

        if (leaveAllocation is null)
            throw new NotFoundException(nameof(leaveAllocation), request.Id);

        await _leaveAllocationeRepository.DeleteAsync(leaveAllocation);
        
        return Unit.Value;
    }
}