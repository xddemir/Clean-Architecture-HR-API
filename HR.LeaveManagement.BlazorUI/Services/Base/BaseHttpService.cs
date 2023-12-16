namespace HR.LeaveManagement.BlazorUI.Services.Base;

public class BaseHttpService
{
    public IClient _Client { get; set; }

    public BaseHttpService(IClient client)
    {
        _Client = client;
    }

    protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
    {
        if (ex.StatusCode == 400)
        {
            return new Response<Guid>()
            {
                Message = "Invalid data was submitted",
                ValidationErrors = ex.Response, Success = false
            };
        }
        else if (ex.StatusCode == 404)
        {
            return new Response<Guid>()
            {
                Message = "Record was not found",
                ValidationErrors = ex.Response, Success = false
            };
        }
        else
        {
            return new Response<Guid>()
            {
                Message = "Something went wrong, please try again",
                ValidationErrors = ex.Response, Success = false
            };
        }
    }
    
}