using SocailMediaApp.Utils;
using SocailMediaApp.ViewModels;
using Swashbuckle.AspNetCore.Filters;

namespace SocailMediaApp.Docs
{
    public class GetAllUsersSuccessfulResponseExample : IExamplesProvider<ApiResponse<Object>>
    {
        public ApiResponse<Object> GetExamples()
        {
            return new ApiResponse<Object>
            {
                Body = new List<UserFriendViewModel>
                {
                    new UserFriendViewModel
                    {
                        Id = 1,
                        Name = "John Doe",
                    },
                    new UserFriendViewModel
                    {
                        Id = 2,
                        Name = "Jane Doe",
                    }
                }
            };
        }
    }
}
