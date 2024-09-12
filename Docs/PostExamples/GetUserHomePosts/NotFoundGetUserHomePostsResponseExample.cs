namespace SocailMediaApp.Docs.PostExamples.GetUserHomePosts
{
    using SocailMediaApp.Utils;
    using Swashbuckle.AspNetCore.Filters;
    using System.Net;

    public class NotFoundGetUserHomePostsResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                Body = null,
                Message = "User not found",
                StatusCode = HttpStatusCode.NotFound
            };
        }
    }
}
