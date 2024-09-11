using SocailMediaApp.Utils;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace SocailMediaApp.Docs.AuthExamples.Login
{
    public class LoginInternalServerErrorResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Internal Server Error, Try again later",
                Body = null
            };
        }
    }

}
