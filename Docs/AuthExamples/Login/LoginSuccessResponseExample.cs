using SocailMediaApp.Utils;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace SocailMediaApp.Docs.AuthExamples.Login
{
    public class LoginSuccessResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.Created,
                Message = "Login successful!",
                Body = "your-jwt-token-here"
            };
        }
    }

}
