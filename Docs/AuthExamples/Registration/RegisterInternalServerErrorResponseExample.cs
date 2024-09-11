using SocailMediaApp.Utils;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace SocailMediaApp.Docs.AuthExamples.Registration
{
    public class RegisterInternalServerErrorResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Error while sending mail to your account. Please register again",
                Body = null
            };
        }
    }

}
