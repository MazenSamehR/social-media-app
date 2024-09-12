using SocailMediaApp.Utils;
using Swashbuckle.AspNetCore.Filters;

public class FollowingListBadRequestResponseExample : IExamplesProvider<ApiResponse<object>>
{
    public ApiResponse<object> GetExamples()
    {
        return new ApiResponse<object>
        {
            Body = null,
            Message = "Bad request.",
            StatusCode = System.Net.HttpStatusCode.BadRequest
        };
    }
}
