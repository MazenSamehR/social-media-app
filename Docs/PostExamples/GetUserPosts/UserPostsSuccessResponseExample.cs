using SocailMediaApp.Utils;
using SocailMediaApp.ViewModels;
using Swashbuckle.AspNetCore.Filters;

namespace SocailMediaApp.Docs.PostExamples.GetUserPosts
{
    public class UserPostsSuccessResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                Body = new List<ReadPostViewModel>
                {
                    new ReadPostViewModel
                    {
                        Content = "This is a post",
                        UserId = 1,
                        PublishedOn = DateTime.Now
                    }
                },
                Message = "Posts retrieved successfully",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
