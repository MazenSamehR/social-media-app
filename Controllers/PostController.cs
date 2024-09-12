using Microsoft.AspNetCore.Mvc;
using SocailMediaApp.Exceptions;
using SocailMediaApp.Services;
using SocailMediaApp.Utils;
using SocailMediaApp.ViewModels;
using System.Net;

namespace SocailMediaApp.Controllers
{
    [Route("api/v1/posts")]
    public class PostController
    {
        private PostService _postService;
        public PostController(PostService postService)
        {
            _postService = postService;
        }


        [HttpPost]
        public ActionResult<ApiResponse<Object>> CreatePost([FromBody] SavePostViewModel postViewModel)
        {
            try
            {
                _postService.AddPost(postViewModel);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Post created successfully";
                apiResponse.StatusCode = HttpStatusCode.Created;
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
            catch(Exception e)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }


        }

        [HttpGet]
        public ActionResult<ApiResponse<Object>> GetAllPosts()
        {
            try
            {
                List<ReadPostViewModel> posts = _postService.GetAllPosts();
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = posts;
                apiResponse.Message = "Posts fetched successfully";
                apiResponse.StatusCode = HttpStatusCode.OK;
                return apiResponse;
            }
            catch (Exception e)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }
        [HttpGet("user/{id}")]
        public ActionResult<ApiResponse<Object>> GetUserPosts(int id)
        {
            try
            {
                List<ReadPostViewModel> post = _postService.GetPostsByUserId(id);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = post;
                apiResponse.Message = "Post fetched successfully";
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
            catch (Exception e)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse<Object>> GetPost(int id)
        {
            try
            {
                ReadPostViewModel post = _postService.GetPostById(id);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = post;
                apiResponse.Message = "Post fetched successfully";
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
            catch (Exception e)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }
        
        [HttpGet("home/{id}")]
        public ActionResult<ApiResponse<Object>> GetUserHomePosts(int id)
        {
            try
            {
                List<ReadPostViewModel> posts = _postService.GetPostsByFollowing(id);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = posts;
                apiResponse.Message = "Posts of following list fetched successfully";
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
            catch (Exception e)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }


        [HttpPut("{id}")]
        public ActionResult<ApiResponse<Object>> UpdatePost(int id, [FromBody] UpdatePostViewModel post)
        {
            try
            {
                _postService.UpdatePost(id, post);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Post updated successfully";
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
            catch (Exception e)
            {
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Internal Server Error, Try again later";
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<Object>> DeletePost(int id)
        {
            try
            {
                _postService.DeletePost(id);
                ApiResponse<Object> apiResponse = new ApiResponse<Object>();
                apiResponse.Body = null;
                apiResponse.Message = "Post deleted successfully";
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
            catch (Exception e)
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
