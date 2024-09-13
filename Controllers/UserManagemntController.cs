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
        public ActionResult<ApiResponse<Object>> GetAllUsers()
        {
            try
            {
                List<UserFriendViewModel> users = authService.GetAllUsers();
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
        public ActionResult<ApiResponse<Object>> Register([FromBody] RegisterUserViewModel user)
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
                authService.Register(user,HttpContext.Request);
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
        public ActionResult<ApiResponse<Object>> Login([FromBody] LoginUserViewModel user)
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
                ReturnedUserView returnedUser = authService.Login(user);
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
        public ActionResult<ApiResponse<Object>> UpdateUser(int id, [FromForm] UpdateUserViewModel user)
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
                authService.UpdateUser(id, user);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
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
        public ActionResult<ApiResponse<Object>> Verify(int id)
        {
            try
            {
                authService.Verify(id);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Email Confirmed!";
                apiResponse.StatusCode = HttpStatusCode.OK;
                return apiResponse;
            }
            catch
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }
    }
}
