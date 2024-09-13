using SocailMediaApp.Utils;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

public class InternalServerErrorUpdateCommentResponseExample : IExamplesProvider<ApiResponse<Object>>
{
    public ApiResponse<Object> GetExamples()
    {
        return new ApiResponse<Object>
        {
            Body = null,
            Message = "Internal Server Error, Try again later",
            StatusCode = HttpStatusCode.InternalServerError
        };
    }
}
