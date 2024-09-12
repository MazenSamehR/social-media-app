namespace SocailMediaApp.Docs.FollowingExamples.FollowUser
{
    using SocailMediaApp.Utils;
    using Swashbuckle.AspNetCore.Filters;

    public class FollowNotFoundResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                Body = null,
                Message = "User not found",
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

}
