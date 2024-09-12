using SocailMediaApp.Utils;

namespace SocailMediaApp.Docs.FollowingExamples.FollowUser
{
    public class FollowYourselfesponseExample
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                Body = null,
                Message = "You cannot follow yourself",
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }
}
