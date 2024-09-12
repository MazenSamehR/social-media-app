using SocailMediaApp.Utils;
using Swashbuckle.AspNetCore.Filters;

namespace SocailMediaApp.Docs.PostExamples.GetUserPosts
{
    public class InternalServerErrorGetUserPostsResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                Body = null,
                Message = "Internal server error",
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            };
        }
    }
}
