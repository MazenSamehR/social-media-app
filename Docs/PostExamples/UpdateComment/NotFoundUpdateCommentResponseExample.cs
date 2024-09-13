using SocailMediaApp.Utils;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

public class NotFoundUpdateCommentResponseExample : IExamplesProvider<ApiResponse<Object>>
{
    public ApiResponse<Object> GetExamples()
    {
        return new ApiResponse<Object>
        {
            Body = null,
            Message = "Post or comment not found",
            StatusCode = HttpStatusCode.NotFound
        };
    }
}
