using SocailMediaApp.Utils;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace SocailMediaApp.Docs.AuthExamples.Registration
{
    public class RegisterValidationErrorResponseExample : IExamplesProvider<ApiResponse<object>>
    {
        public ApiResponse<object> GetExamples()
        {
            return new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Invalid data!",
                Body = new Dictionary<string, string>
            {
                { "Email", "The email field is required." },
                { "Password", "The password field is required." }
            }
            };
        }
    }

}
