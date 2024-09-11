using Microsoft.AspNetCore.Mvc;
using SocailMediaApp.Exceptions;
using SocailMediaApp.Services;
using SocailMediaApp.Utils;
using SocailMediaApp.ViewModels;
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
        public ActionResult<ApiResponse<Object>> Follow([FromBody] FollowRequestViewModel friendRequest)
        {
            try
            {
                _followingManagementService.FollowUser(friendRequest);
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
        public ActionResult<ApiResponse<Object>> GetFollowingList(int userId)
        {
            try
            {
                List<UserFriendViewModel> followingList = _followingManagementService.GetFollowingList(userId);
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
        public ActionResult<ApiResponse<Object>> GetFollowerList(int userId)
        {
            try
            {
                List<UserFriendViewModel> followersList = _followingManagementService.GetFollowersList(userId);
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
        public ActionResult<ApiResponse<Object>> Unfollow([FromBody] FollowRequestViewModel friendRequest)
        {
            try
            {
                _followingManagementService.Unfollow(friendRequest);
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
