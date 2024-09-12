namespace SocailMediaApp.Docs.PostExamples.UpdatePost
{
    using SocailMediaApp.Utils;
    using Swashbuckle.AspNetCore.Filters;
    using System.Net;

    public class SuccessfulUpdatePostResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                Body = null,
                Message = "Post updated successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
