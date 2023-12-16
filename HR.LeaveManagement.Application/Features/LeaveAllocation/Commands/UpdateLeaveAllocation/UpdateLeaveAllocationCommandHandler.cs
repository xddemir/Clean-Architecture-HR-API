using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler: IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly ILeaveAllocationeRepository _leaveAllocationeRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<UpdateLeaveAllocationCommandHandler> _logger;

    public UpdateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationeRepository leaveAllocationeRepository, ILeaveTypeRepository leaveTypeRepository, IAppLogger<UpdateLeaveAllocationCommandHandler> logger)
    {
        _mapper = mapper;
        _leaveAllocationeRepository = leaveAllocationeRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationeRepository.GetByIdAsync(request.Id);

        if (leaveAllocation is null)
            throw new NotFoundException(nameof(Domain.LeaveAllocation), request.Id);
    
        var validator = new UpdateLeaveAllocationCommandValidator(_leaveTypeRepository, _leaveAllocationeRepository);
        var validations = await validator.ValidateAsync(request);

        if (validations.Errors.Any())
            throw new BadRequestException("Invalid Leave Allocation", validations);

        _mapper.Map(request, leaveAllocation);

        await _leaveAllocationeRepository.UpdateAsync(leaveAllocation);

        var email = new EmailMessage()
        {
            To = string.Empty,
            Body = "",
            Subject = "Leave Request updated"
        };

        await _emailSender.SendEmail(email);

        return Unit.Value;
    }
}