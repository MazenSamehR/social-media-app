using SocailMediaApp.Utils;
using Swashbuckle.AspNetCore.Filters;

namespace SocailMediaApp.Docs.PostExamples.GetSinglePost
{
    public class GetSinglePostInternalServerErrorResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                Body = null,
                Message = "Internal Server Error, Try again later",
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            };
        }
    }
}
