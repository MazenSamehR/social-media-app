namespace SocailMediaApp.Docs.FollowingExamples.FollowUser
{
    using SocailMediaApp.Utils;
    using Swashbuckle.AspNetCore.Filters;

    public class FollowBadRequestResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                Body = null,
                Message = "You are already following this user.",
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

}
