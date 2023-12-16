namespace HR.LeaveManagement.BlazorUI.Services.Base;

public record Response<T>()
{
    public string Message;
    public  string ValidationErrors;
    public  bool Success;
    public  T Data;
};