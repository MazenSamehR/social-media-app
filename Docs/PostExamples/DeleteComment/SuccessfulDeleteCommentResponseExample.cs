using SocailMediaApp.Utils;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

public class SuccessfulDeleteCommentResponseExample : IExamplesProvider<ApiResponse<Object>>
{
    public ApiResponse<Object> GetExamples()
    {
        return new ApiResponse<Object>
        {
            Body = null,
            Message = "Comment deleted successfully",
            StatusCode = HttpStatusCode.OK
        };
    }
}
