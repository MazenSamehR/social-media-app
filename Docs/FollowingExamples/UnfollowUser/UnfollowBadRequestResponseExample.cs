using SocailMediaApp.Utils;
using Swashbuckle.AspNetCore.Filters;

public class UnfollowBadRequestResponseExample : IExamplesProvider<ApiResponse<object>>
{
    public ApiResponse<object> GetExamples()
    {
        return new ApiResponse<object>
        {
            Body = null,
            Message = "You cannot follow yourself",
            StatusCode = System.Net.HttpStatusCode.BadRequest
        };
    }
}
