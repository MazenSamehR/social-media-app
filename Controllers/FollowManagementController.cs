using Microsoft.AspNetCore.Mvc;
using SocailMediaApp.Docs.FollowExamples.FollowUser;
using SocailMediaApp.Docs.FollowingExamples.FollowUser;
using SocailMediaApp.Exceptions;
using SocailMediaApp.Services;
using SocailMediaApp.Utils;
using SocailMediaApp.ViewModels;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace SocailMediaApp.Controllers
{
    [Route("api/v1/follow-management")]
    public class FollowingController : ControllerBase
    {
        private FollowingManagementService _followingManagementService;

        public FollowingController(FollowingManagementService _followingManagementService)
        {
            this._followingManagementService = _followingManagementService;
        }

        [HttpPost("follow")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(FollowSuccessResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(FollowNotFoundResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(FollowBadRequestResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(FollowYourselfesponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(FollowInternalServerErrorResponseExample))]
        public async Task<ActionResult<ApiResponse<Object>>> Follow([FromBody] FollowRequestViewModel friendRequest)
        {
            try
            {
                await _followingManagementService.FollowUser(friendRequest);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Followed successfully!";
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
            catch (AlreadyExistesException ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return apiResponse;
            }
            catch(InvalidOperationException ex)
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
        [HttpGet("following/{userId}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(FollowingListSuccessResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(FollowingListNotFoundResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(FollowingListBadRequestResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(FollowingListInternalServerErrorResponseExample))]

        public async Task<ActionResult<ApiResponse<Object>>> GetFollowingList(int userId)
        {
            try
            {
                List<UserFriendViewModel> followingList = await _followingManagementService.GetFollowingList(userId);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = followingList;
                apiResponse.Message = "Followeing list fetched successfully!";
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
        
        [HttpGet("follower/{userId}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(FollowingListSuccessResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(FollowingListNotFoundResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(FollowingListBadRequestResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(FollowingListInternalServerErrorResponseExample))]

        public async Task<ActionResult<ApiResponse<Object>>> GetFollowerList(int userId)
        {
            try
            {
                List<UserFriendViewModel> followersList = await _followingManagementService.GetFollowersList(userId);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = followersList;
                apiResponse.Message = "Follower list fetched successfully!";
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
        [HttpPost("unfollow")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UnfollowSuccessResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(UnfollowNotFoundResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(UnfollowBadRequestResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(UnfollowInternalServerErrorResponseExample))]

        public async Task<ActionResult<ApiResponse<Object>>> Unfollow([FromBody] FollowRequestViewModel friendRequest)
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
                await _followingManagementService.Unfollow(friendRequest);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Unfollowed successfully!";
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
            catch (AlreadyExistesException ex)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return apiResponse;
            }
            catch (InvalidOperationException ex)
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
    }
}
