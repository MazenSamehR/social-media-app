namespace SocailMediaApp.Docs.PostExamples.GetUserHomePosts
{
    using SocailMediaApp.Utils;
    using SocailMediaApp.ViewModels;
    using Swashbuckle.AspNetCore.Filters;
    using System;
    using System.Collections.Generic;
    using System.Net;

    public class SuccessfulGetUserHomePostsResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            var posts = new List<ReadPostViewModel>
            {
                new ReadPostViewModel
                {
                    Content = "This is a home post content.",
                    UserId = 1,
                    PublishedOn = DateTime.UtcNow
                },
                new ReadPostViewModel
                {
                    Content = "Another home post content.",
                    UserId = 2,
                    PublishedOn = DateTime.UtcNow
                }
            };

            return new ApiResponse<object>
            {
                Body = posts,
                Message = "Posts of following list fetched successfully",
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
