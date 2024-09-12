using SocailMediaApp.Utils;
using Swashbuckle.AspNetCore.Filters;

public class UnfollowSuccessResponseExample : IExamplesProvider<ApiResponse<object>>
{
    public ApiResponse<object> GetExamples()
    {
        return new ApiResponse<object>
        {
            Body = null,
            Message = "Unfollowed successfully!",
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }
}
