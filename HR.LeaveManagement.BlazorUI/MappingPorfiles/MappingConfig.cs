using AutoMapper;
using HR.LeaveManagement.BlazorUI.Models.LeaveType;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.MappingPorfiles;

public class MappingConfig: Profile
{
    public MappingConfig()
    {
        CreateMap<LeaveTypeDto, LeaveTypeViewModel>().ReverseMap();
        CreateMap<CreateLeaveTypeCommand, LeaveTypeViewModel>().ReverseMap();
        CreateMap<UpdateLeaveTypeCommand, LeaveTypeViewModel>().ReverseMap();
    }
}