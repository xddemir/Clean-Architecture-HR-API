using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesDetailsQueryHandler : IRequestHandler<GetLeaveTypesDetailsQuery,LeaveTypeDetailDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<GetLeaveTypesDetailsQueryHandler> _logger;

    public GetLeaveTypesDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<GetLeaveTypesDetailsQueryHandler> logger)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<LeaveTypeDetailDto> Handle(GetLeaveTypesDetailsQuery request, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.Id) ??
                        throw new NotFoundException(nameof(Domain.LeaveType), request);

        var response = _mapper.Map<LeaveTypeDetailDto>(leaveType);

        _logger.LoginInformation("Leave types were retrieved successfully");

        return response;
    }
}