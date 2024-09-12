namespace SocailMediaApp.Docs.PostExamples.DeletePost
{
    using SocailMediaApp.Utils;
    using Swashbuckle.AspNetCore.Filters;
    using System.Net;

    public class NotFoundDeletePostResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                Body = null,
                Message = "Post not found.",
                StatusCode = HttpStatusCode.NotFound
            };
        }
    }
}
