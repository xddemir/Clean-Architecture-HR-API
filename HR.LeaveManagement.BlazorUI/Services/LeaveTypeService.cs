using AutoMapper;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveType;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Services;

public class LeaveTypeService: BaseHttpService, ILeaveTypeService
{
    private readonly IMapper _mapper;

    public LeaveTypeService(IClient client, IMapper mapper) : base(client)
    {
        _mapper = mapper;
    }

    public async Task<List<LeaveTypeViewModel>> GetLeaveTypes()
    {
        var leaveTypes = await _Client.LeaveTypesAllAsync();
        return _mapper.Map<List<LeaveTypeViewModel>>(leaveTypes);
    }

    public async Task<LeaveTypeViewModel> GetLeaveTypeDetails(int id)
    {
        var leaveTypes = await _Client.LeaveTypesGETAsync(id);
        return _mapper.Map<LeaveTypeViewModel>(leaveTypes);
    }

    public async Task<Response<Guid>> CreateLeaveType(LeaveTypeViewModel leaveType)
    {
        try
        {
            var createLeaveTypeCom = _mapper.Map<CreateLeaveTypeCommand>(leaveType);
            await _Client.LeaveTypesPOSTAsync(createLeaveTypeCom);
            return new Response<Guid>()
            {
                Success = true
            };
        }
        catch (ApiException e)
        {
            return ConvertApiExceptions<Guid>(e);
        }
    }

    public async Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeViewModel leaveType)
    {
        try
        {
            var updateLeaveTypeCommand = _mapper.Map<UpdateLeaveTypeCommand>(leaveType);
            await _Client.LeaveTypesPUTAsync(id.ToString(), updateLeaveTypeCommand);
            return new Response<Guid>()
            {
                Success = true
            };
        }
        catch (ApiException e)
        {
            return ConvertApiExceptions<Guid>(e);
        }
    }

    public async Task<Response<Guid>> DeleteLeaveType(int id)
    {
        try
        {
            await _Client.LeaveTypesDELETEAsync(id);
            return new Response<Guid>()
            {
                Success = true
            };
        }
        catch (ApiException e)
        {
            return ConvertApiExceptions<Guid>(e);
        }
    }
}