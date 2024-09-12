using SocailMediaApp.Utils;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace SocailMediaApp.Docs.PostExamples.GetUserPosts
{
    public class NotFoundGetUserPostsResponseExample : IExamplesProvider<ApiResponse<object>>
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
