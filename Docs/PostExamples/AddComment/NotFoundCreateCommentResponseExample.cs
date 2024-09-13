using SocailMediaApp.Utils;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

public class NotFoundCreateCommentResponseExample : IExamplesProvider<ApiResponse<Object>>
{
    public ApiResponse<Object> GetExamples()
    {
        return new ApiResponse<Object>
        {
            Body = null,
            Message = "Post not found",
            StatusCode = HttpStatusCode.NotFound
        };
    }
}
