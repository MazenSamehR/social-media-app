using SocailMediaApp.Utils;
using Swashbuckle.AspNetCore.Filters;

public class FollowingListNotFoundResponseExample : IExamplesProvider<ApiResponse<object>>
{
    public ApiResponse<object> GetExamples()
    {
        return new ApiResponse<object>
        {
            Body = null,
            Message = "User not found!",
            StatusCode = System.Net.HttpStatusCode.NotFound
        };
    }
}
