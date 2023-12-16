﻿using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandValidator: AbstractValidator<UpdateLeaveRequestCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public UpdateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        
        Include(new BaseLeaveRequestValidator(_leaveTypeRepository));

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveRequestMustExist)
            .WithMessage("{PropertyName} must be parent");

    }

    private async Task<bool> LeaveRequestMustExist(int Id, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveRequestRepository.GetByIdAsync(Id);
        return leaveAllocation != null;
    }
}