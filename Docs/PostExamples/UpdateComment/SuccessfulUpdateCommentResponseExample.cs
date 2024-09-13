using SocailMediaApp.Utils;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

public class SuccessfulUpdateCommentResponseExample : IExamplesProvider<ApiResponse<Object>>
{
    public ApiResponse<Object> GetExamples()
    {
        return new ApiResponse<Object>
        {
            Body = null,
            Message = "Comment updated successfully",
            StatusCode = HttpStatusCode.OK
        };
    }
}
