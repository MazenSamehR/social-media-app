using Microsoft.AspNetCore.Mvc;
using SocailMediaApp.Docs;
using SocailMediaApp.Docs.AuthExamples.Login;
using SocailMediaApp.Docs.AuthExamples.Registration;
using SocailMediaApp.Docs.AuthExamples.Verification;
using SocailMediaApp.Exceptions;
using SocailMediaApp.Models;
using SocailMediaApp.Services;
using SocailMediaApp.Utils;
using SocailMediaApp.ViewModels;
using Swashbuckle.AspNetCore.Filters;
using System.Net;


namespace SocailMediaApp.Controllers
{

    [Route("api/v1/users")]
    public class UserManagemntController : ControllerBase
    {
        private UserService authService;

        public UserManagemntController(UserService authService)
        {
            this.authService = authService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllUsersSuccessfulResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorDeleteCommentResponseExample))]
        public async Task<ActionResult<ApiResponse<Object>>> GetAllUsers()
        {
            try
            {
                List<UserFriendViewModel> users = await authService.GetAllUsers();
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = users;
                apiResponse.Message = "Users fetched!";
                apiResponse.StatusCode = HttpStatusCode.OK;
                return apiResponse;
            }
            catch(Exception ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }


        
        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(RegisterSuccessResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(RegisterValidationErrorResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(RegisterInternalServerErrorResponseExample))]
        public async Task<ActionResult<ApiResponse<Object>>> Register([FromBody] RegisterUserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key, 
                        kvp => string.Join("; ", kvp.Value.Errors.Select(e => e.ErrorMessage)) 
                        );

                ApiResponse<object> validationResponse = new ApiResponse<object>
                {
                    Body = errors, 
                    Message = "Invalid data!",
                    StatusCode = HttpStatusCode.BadRequest
                };

                return validationResponse;
            }
            try
            {
                await authService.Register(user,HttpContext.Request);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "User Registered, Check your mail to confirm";
                apiResponse.StatusCode = HttpStatusCode.Created;
                return apiResponse;
            }
            catch (AlreadyExistesException ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return apiResponse;
            }
            catch(MailConfirmationException ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Error while sending mail to your account. Please register again";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
            catch (Exception ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
            

        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(LoginSuccessResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(LoginValidationErrorResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(LoginNotFoundErrorResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(LoginInternalServerErrorResponseExample))]
        public async Task<ActionResult<ApiResponse<Object>>> Login([FromBody] LoginUserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => string.Join("; ", kvp.Value.Errors.Select(e => e.ErrorMessage))
                        );

                ApiResponse<object> validationResponse = new ApiResponse<object>
                {
                    Body = errors,
                    Message = "Invalid data!",
                    StatusCode = HttpStatusCode.BadRequest
                };

                return validationResponse;
            }
            try
            {
                ReturnedUserView returnedUser = await authService.Login(user);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = returnedUser;
                apiResponse.Message = "Login successful!";
                apiResponse.StatusCode = HttpStatusCode.Created;
                return apiResponse;
            }
            catch(InvalidException ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return apiResponse;
            }
            catch(NotFoundException ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.NotFound;
                return apiResponse;
            }
            catch (Exception ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }

        // update user details including a profile picture
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<Object>), StatusCodes.Status500InternalServerError)]
    /*    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UpdateUserSuccessResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(UpdateUserValidationErrorResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(UpdateUserNotFoundResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(UpdateUserInternalServerErrorResponseExample))]
       */
        public async Task<ActionResult<ApiResponse<Object>>> UpdateUser(int id, [FromForm] UpdateUserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .ToDictionary(
                                    kvp => kvp.Key,
                                    kvp => string.Join("; ", kvp.Value.Errors.Select(e => e.ErrorMessage))
                                );

                ApiResponse<object> validationResponse = new ApiResponse<object>
                {
                    Body = errors,
                    Message = "Invalid data!",
                    StatusCode = HttpStatusCode.BadRequest
                };

                return validationResponse;
            }
            try
            {
                string? profileImageUrl = await authService.UpdateUser(id, user);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = profileImageUrl;
                apiResponse.Message = "User Updated!";
                apiResponse.StatusCode = HttpStatusCode.OK;
                return apiResponse;
            }
            catch (NotFoundException ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.NotFound;
                return apiResponse;
            }
            catch (Exception ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }

        [HttpGet("verify/{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(VerifySuccessResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(VerifyInternalServerErrorResponseExample))]
        public async Task<ActionResult> Verify(int id)
        {
            try
            {
                await authService.Verify(id);

                var htmlResponse = $@"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Email Confirmation - Success</title>
                <style>
                    body {{ font-family: Arial, sans-serif; margin: 0; padding: 20px; background-color: #e9f5f6; color: #333; }}
                    .container {{ max-width: 600px; margin: auto; padding: 20px; background: #ffffff; border: 1px solid #ddd; border-radius: 8px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1); }}
                    h1 {{ color: #28a745; margin-top: 0; }}
                    p {{ margin-bottom: 0; }}
                    .button {{ display: inline-block; padding: 10px 20px; margin-top: 20px; background-color: #28a745; color: #fff; text-decoration: none; border-radius: 4px; }}
                    .button:hover {{ background-color: #218838; }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h1>Email Confirmed!</h1>
                    <p>Your email address has been successfully verified. You can now proceed with the next steps.</p>
                    <a href='/' class='button'>Go to Homepage</a>
                </div>
            </body>
            </html>";

                return new ContentResult
                {
                    Content = htmlResponse,
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch
            {
                var htmlResponse = $@"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Email Confirmation - Error</title>
                <style>
                    body {{ font-family: Arial, sans-serif; margin: 0; padding: 20px; background-color: #f2f4f7; color: #333; }}
                    .container {{ max-width: 600px; margin: auto; padding: 20px; background: #ffffff; border: 1px solid #ddd; border-radius: 8px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1); }}
                    h1 {{ color: #dc3545; margin-top: 0; }}
                    p {{ margin-bottom: 0; }}
                    .button {{ display: inline-block; padding: 10px 20px; margin-top: 20px; background-color: #dc3545; color: #fff; text-decoration: none; border-radius: 4px; }}
                    .button:hover {{ background-color: #c82333; }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h1>Internal Server Error</h1>
                    <p>We encountered an issue while processing your request. Please try again later.</p>
                    <a href='/' class='button'>Go to Homepage</a>
                </div>
            </body>
            </html>";

                return new ContentResult
                {
                    Content = htmlResponse,
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }


    }
}
