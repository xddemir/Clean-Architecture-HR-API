namespace HR.LeaveManagement.Application.Contracts.Logging;

public interface IAppLogger<T>
{
    void LoginInformation(string message, params object[] args);
    void LogWarning(string message, params object[] args);
}