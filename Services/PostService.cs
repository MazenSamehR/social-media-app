using Microsoft.Extensions.Hosting;
using SocailMediaApp.Exceptions;
using SocailMediaApp.Models;
using SocailMediaApp.Repositories;
using SocailMediaApp.ViewModels;

namespace SocailMediaApp.Services
{
    public class PostService
    {
        private PostRepository _postRepository;
        private UserRepository _userRepository;
        private ImageService _imageService;
        public PostService(PostRepository postRepository, UserRepository userRepository, ImageService imageService)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _imageService = imageService;
        }

        private async Task<List<ReadCommentViewModel>> AddCommentViewModel(Post post)
        {
            List<ReadCommentViewModel> commentViewModels = new List<ReadCommentViewModel>();
            foreach (var comment in post.Comments)
            {
                User? author = await _userRepository.GetUserById(comment.UserId);
                UserFriendViewModel userFriendViewModel = new UserFriendViewModel
                {
                    Id = author.Id,
                    Name = author.Name,
                    ProfileImageUrl = author.ProfileImageUrl
                };
                ReadCommentViewModel commentViewModel = new ReadCommentViewModel
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    Author = userFriendViewModel,
                    PublishedOn = comment.PublishedOn
                };
                commentViewModels.Add(commentViewModel);
            }
            return commentViewModels;
        }

        private async Task<ReadPostViewModel> GetReadPostViewModel(Post post)
        {
            List<ReadCommentViewModel> commentViewModels = await AddCommentViewModel(post);

            ReadPostViewModel postViewModel = new ReadPostViewModel
            {
                Id = post.Id,
                Content = post.Content,
                UserId = post.UserId,
                PublishedOn = post.PublishedOn,
                PostImageUrl = post.ImageUrl,
                Comments = commentViewModels
            };
            // get user
            User? user = await _userRepository.GetUserById(post.UserId);
            postViewModel.ProfileImageUrl = user.ProfileImageUrl;
            postViewModel.AuthorName = user.Name;
            return postViewModel;
        }

        public async Task AddPost(SavePostViewModel post)
        {
            Post convertedPost = new Post();
            User? foundUser = await _userRepository.GetUserById(post.UserId);
            if (foundUser == null)
                throw new NotFoundException("User not found");
            convertedPost.UserId = post.UserId;
            convertedPost.Author = foundUser;
            convertedPost.Content = post.Content;
            convertedPost.PublishedOn = DateTime.Now;
            if(post.Image != null)
            {
                convertedPost.ImageUrl = await _imageService.UploadImage(post.Image);
            }
            await _postRepository.AddPost(convertedPost);
        }

        public async Task<ReadPostViewModel> GetPostById(int id)
        {
            Post? foundPost = await _postRepository.GetPostById(id);
            if (foundPost == null)
                throw new NotFoundException("Post not found");
            ReadPostViewModel postViewModel = await GetReadPostViewModel(foundPost);
            return postViewModel;
        }
        public async Task<List<ReadPostViewModel>> GetPostsByUserId(int userId)
        {
            User? foundUser = await _userRepository.GetUserById(userId);
            if (foundUser == null)
                throw new NotFoundException("User not found");
            List<Post> posts = await _postRepository.GetPostsByUserId(userId);
            posts.ForEach(post =>
            {
                post.Comments = _postRepository.GetCommentsByPostId(post.Id).Result;
            });
            List<ReadPostViewModel> postViewModels = new List<ReadPostViewModel>();
            foreach (var post in posts)
            {
                ReadPostViewModel postViewModel = await GetReadPostViewModel(post);

                postViewModels.Add(postViewModel);
            }
            return postViewModels;
        }

        public async Task<List<ReadPostViewModel>> GetPostsByFollowing(int userId)
        {
            User? foundUser = await _userRepository.GetUserById(userId);
            if (foundUser == null)
                throw new NotFoundException("User not found");
            List<string> followingListEmails = _userRepository.GetAllFollowing(userId).Result.Select(u => u.Email).ToList();
            List<User> allUsers = await _userRepository.GetAllUsers();
            List<ReadPostViewModel> allPosts = new List<ReadPostViewModel>();
            List<ReadPostViewModel> nonFollowingPosts = new List<ReadPostViewModel>();
            foreach(var user in allUsers)
            {
                if(user.Email.Equals(foundUser.Email))
                {
                    continue;
                }
                List<ReadPostViewModel> posts = await GetPostsByUserId(user.Id);
                if (followingListEmails.Contains(user.Email))
                {
                    foreach(var post in posts)
                    {
                        allPosts.Add(post);
                    }
                }
                else
                {
                    foreach (var post in posts)
                    {
                        nonFollowingPosts.Add(post);
                    }
                }
            }
            allPosts.AddRange(nonFollowingPosts);
            return allPosts;
        }



        public async Task<List<ReadPostViewModel>> GetAllPosts()
        {
            List<Post> posts = await _postRepository.GetAllPosts();
            List<ReadPostViewModel> postViewModels = new List<ReadPostViewModel>();
            foreach (var post in posts)
            {
                
                ReadPostViewModel postViewModel = await GetReadPostViewModel(post);

                postViewModels.Add(postViewModel);
            }
            return postViewModels;
        }

        public async Task UpdatePost(int id, UpdatePostViewModel post)
        {
            Post? foundPost = await _postRepository.GetPostById(id);
            if (foundPost == null)
                throw new NotFoundException("Post not found");
            foundPost.Content = post.Content;
            await _postRepository.UpdatePost(foundPost);
        }

        public async Task AddCommentToPost(SaveCommentViewModel comment)
        {
            int postId = comment.PostId;
            Post? foundPost = await _postRepository.GetPostById(postId);
            if (foundPost == null)
                throw new NotFoundException("Post not found");
            User? foundUser = await _userRepository.GetUserById(comment.UserId);
            if (foundUser == null)
                throw new NotFoundException("User not found");
            Comment convertedComment = new Comment();
            convertedComment.UserId = comment.UserId;
            convertedComment.Author = foundUser;
            convertedComment.Content = comment.Content;
            convertedComment.PublishedOn = DateTime.Now;
            convertedComment.PostId = postId;
            foundPost.Comments.Add(convertedComment);
            await _postRepository.AddCommentToPost(convertedComment, foundPost);
        }
        public async Task UpdateCommentInPost(int postId, ChangedCommentViewModel comment)
        {
            Post? foundPost = await _postRepository.GetPostById(postId);
            if (foundPost == null)
                throw new NotFoundException("Post not found");
            Comment? convertedComment = await _postRepository.GetCommendById(postId, comment.CommendtId);
            if(convertedComment == null)
                throw new NotFoundException("Comment not found");
            convertedComment.Content = comment.Content;
            await _postRepository.UpdateCommentInPost(convertedComment, foundPost);
        }

        public async Task DeleteCommentFromPost(int postId, DeleteCommentViewModel comment)
        {
            Post? foundPost = await _postRepository.GetPostById(postId);
            if (foundPost == null)
                throw new NotFoundException("Post not found");
            Comment? convertedComment = await _postRepository.GetCommendById(postId, comment.CommentId);
            if (convertedComment == null)
                throw new NotFoundException("Comment not found");

            await _postRepository.DeleteCommendtFromPost(convertedComment, foundPost);
        }


        public async Task DeletePost(int id)
        {
            Post? foundPost = await _postRepository.GetPostById(id);
            if (foundPost == null)
                throw new NotFoundException("Post not found");
            await _postRepository.DeletePost(id);
        }

    }
}
