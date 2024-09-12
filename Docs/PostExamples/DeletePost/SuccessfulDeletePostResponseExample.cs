namespace SocailMediaApp.Docs.PostExamples.DeletePost
{
    using SocailMediaApp.Utils;
    using Swashbuckle.AspNetCore.Filters;
    using System.Net;

    public class SuccessfulDeletePostResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                Body = null,
                Message = "Post deleted successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
