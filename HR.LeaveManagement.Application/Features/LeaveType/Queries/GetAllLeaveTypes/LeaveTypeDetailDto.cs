namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class LeaveTypeDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DefaultDays { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
    
}