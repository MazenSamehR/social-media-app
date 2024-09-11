using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocailMediaApp.Exceptions;
using SocailMediaApp.Models;
using SocailMediaApp.Services;
using SocailMediaApp.Utils;
using SocailMediaApp.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace SocailMediaApp.Controllers
{
    
    [Route("api/v1/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private static AuthService authService = new AuthService();


        [HttpGet]
        public ActionResult<ApiResponse<List<User>>> GetAllUsers()
        {

            List<User> users = authService.GetAllUsers();
            ApiResponse<List<User>> apiResponse = new ApiResponse<List<User>>();
            apiResponse.Body = users;
            apiResponse.Message = "Users fetched!";
            apiResponse.StatusCode = HttpStatusCode.OK;
            return apiResponse;
        }

        [HttpPost("register")]
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
                authService.Register(user);
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
                String token = authService.Login(user);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = token;
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

        [HttpPost("verify/{id}")]
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
