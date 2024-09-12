using SocailMediaApp.Utils;
using SocailMediaApp.ViewModels;
using Swashbuckle.AspNetCore.Filters;
public class FollowingListSuccessResponseExample : IExamplesProvider<ApiResponse<object>>
{
    public ApiResponse<object> GetExamples()
    {
        return new ApiResponse<object>
        {
            Body = new List<UserFriendViewModel>
            {
                new UserFriendViewModel { Id = 1, Name = "JohnDoe" },
                new UserFriendViewModel { Id  = 2, Name = "JaneDoe" }
            },
            Message = "Following list fetched successfully!",
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }
}
