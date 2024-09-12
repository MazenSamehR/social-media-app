namespace SocailMediaApp.Docs.FollowingExamples.FollowUser
{
    using SocailMediaApp.Utils;
    using Swashbuckle.AspNetCore.Filters;

    public class FollowInternalServerErrorResponseExample : IExamplesProvider<ApiResponse<object>>
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
