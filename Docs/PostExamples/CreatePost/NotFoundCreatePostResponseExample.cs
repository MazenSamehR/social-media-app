namespace SocailMediaApp.Docs.PostExamples.CreatePost
{
    using SocailMediaApp.Utils;
    using Swashbuckle.AspNetCore.Filters;
    using System.Net;

    public class NotFoundCreatePostResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                Body = null,
                Message = "Post not found",
                StatusCode = HttpStatusCode.NotFound
            };
        }
    }
}
