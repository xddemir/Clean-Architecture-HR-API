using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistance;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator: AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationeRepository _allocationeRepository;

    public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationeRepository allocationeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _allocationeRepository = allocationeRepository;

        RuleFor(p => p.NumberOfDays)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must greater than {ComparisonValue}");

        RuleFor(p => p.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}");

        RuleFor(p => p.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName does not exist}");

        RuleFor(p => p.Id).MustAsync(LeaveAllocationMustExist).NotNull().WithMessage("{PropertyName must be present}");

    }

    private async Task<bool> LeaveAllocationMustExist(int id, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _allocationeRepository.GetByIdAsync(id);
        return leaveAllocation != null;
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
        return leaveType != null;
    }
}