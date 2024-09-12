namespace SocailMediaApp.Docs.FollowExamples.FollowUser
{
    using SocailMediaApp.Utils;
    using Swashbuckle.AspNetCore.Filters;

    public class FollowSuccessResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                Body = null,
                Message = "Followed successfully",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }

}
