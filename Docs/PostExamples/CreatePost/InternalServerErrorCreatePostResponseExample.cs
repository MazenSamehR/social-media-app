namespace SocailMediaApp.Docs.PostExamples.CreatePost
{
    using SocailMediaApp.Utils;
    using Swashbuckle.AspNetCore.Filters;
    using System.Net;

    public class InternalServerErrorCreatePostResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                Body = null,
                Message = "Internal Server Error, Try again later",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}
