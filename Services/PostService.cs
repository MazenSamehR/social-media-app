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
        public PostService(PostRepository postRepository, UserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        private List<ReadCommentViewModel> AddCommentViewModel(Post post)
        {
            List<ReadCommentViewModel> commentViewModels = new List<ReadCommentViewModel>();
            foreach (var comment in post.Comments)
            {
                UserFriendViewModel userFriendViewModel = new UserFriendViewModel
                {
                    Id = comment.Author.Id,
                    Name = comment.Author.Name,
                    ProfileImageUrl = comment.Author.ProfileImageUrl
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

        private ReadPostViewModel GetReadPostViewModel(Post post)
        {
            List<ReadCommentViewModel> commentViewModels = AddCommentViewModel(post);

            ReadPostViewModel postViewModel = new ReadPostViewModel
            {
                Id = post.Id,
                Content = post.Content,
                UserId = post.UserId,
                PublishedOn = post.PublishedOn,
                ProfileImageUrl = post.Author.ProfileImageUrl,
                Comments = commentViewModels
            };
            return postViewModel;
        }

        public void AddPost(SavePostViewModel post)
        {
            Post convertedPost = new Post();
            User? foundUser = _userRepository.GetUserById(post.UserId);
            if (foundUser == null)
                throw new NotFoundException("User not found");
            convertedPost.UserId = post.UserId;
            convertedPost.Author = foundUser;
            convertedPost.Content = post.Content;
            convertedPost.PublishedOn = DateTime.Now;
            _postRepository.AddPost(convertedPost);
        }

        public ReadPostViewModel GetPostById(int id)
        {
            Post? foundPost = _postRepository.GetPostById(id);
            if (foundPost == null)
                throw new NotFoundException("Post not found");
            ReadPostViewModel postViewModel = GetReadPostViewModel(foundPost);
            return postViewModel;
        }
        public List<ReadPostViewModel> GetPostsByUserId(int userId)
        {
            User? foundUser = _userRepository.GetUserById(userId);
            if (foundUser == null)
                throw new NotFoundException("User not found");
            List<Post> posts = _postRepository.GetPostsByUserId(userId);
            List<ReadPostViewModel> postViewModels = new List<ReadPostViewModel>();
            foreach (var post in posts)
            {
                ReadPostViewModel postViewModel = GetReadPostViewModel(post);

                postViewModels.Add(postViewModel);
            }
            return postViewModels;
        }

        public List<ReadPostViewModel> GetPostsByFollowing(int userId)
        {
            User? foundUser = _userRepository.GetUserById(userId);
            if (foundUser == null)
                throw new NotFoundException("User not found");
            List<User> followingList = foundUser.Following;
            List<User> allUsers = _userRepository.GetAllUsers();
            List<ReadPostViewModel> allPosts = new List<ReadPostViewModel>();
            List<ReadPostViewModel> nonFollowingPosts = new List<ReadPostViewModel>();
            foreach(var user in allUsers)
            {
                if(user.Equals(foundUser))
                {
                    continue;
                }
                List<ReadPostViewModel> posts = GetPostsByUserId(user.Id);
                if (followingList.Contains(user))
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



        public List<ReadPostViewModel> GetAllPosts()
        {
            List<Post> posts = _postRepository.GetAllPosts();
            List<ReadPostViewModel> postViewModels = new List<ReadPostViewModel>();
            foreach (var post in posts)
            {
                
                ReadPostViewModel postViewModel = GetReadPostViewModel(post);

                postViewModels.Add(postViewModel);
            }
            return postViewModels;
        }

        public void UpdatePost(int id, UpdatePostViewModel post)
        {
            Post? foundPost = _postRepository.GetPostById(id);
            if (foundPost == null)
                throw new NotFoundException("Post not found");
            foundPost.Content = post.Content;
            _postRepository.UpdatePost(foundPost);
        }

        public void AddCommentToPost(SaveCommentViewModel comment)
        {
            int postId = comment.PostId;
            Post? foundPost = _postRepository.GetPostById(postId);
            if (foundPost == null)
                throw new NotFoundException("Post not found");
            User? foundUser = _userRepository.GetUserById(comment.UserId);
            if (foundUser == null)
                throw new NotFoundException("User not found");
            Comment convertedComment = new Comment();
            convertedComment.Id = foundPost.Comments.Count + 1;
            convertedComment.UserId = comment.UserId;
            convertedComment.Author = foundUser;
            convertedComment.Content = comment.Content;
            convertedComment.PublishedOn = DateTime.Now;
            convertedComment.PostId = postId;
            foundPost.Comments.Add(convertedComment);
        }
        public void UpdateCommentInPost(int postId, ChangedCommentViewModel comment)
        {
            Post? foundPost = _postRepository.GetPostById(postId);
            if (foundPost == null)
                throw new NotFoundException("Post not found");
            Comment? convertedComment = _postRepository.GetCommendById(postId, comment.CommendtId);
            if(convertedComment == null)
                throw new NotFoundException("Comment not found");
            convertedComment.Content = comment.Content;
            _postRepository.UpdateCommentInPost(convertedComment,foundPost);
        }

        public void DeleteCommentFromPost(int postId, ChangedCommentViewModel comment)
        {
            Post? foundPost = _postRepository.GetPostById(postId);
            if (foundPost == null)
                throw new NotFoundException("Post not found");
            Comment? convertedComment = _postRepository.GetCommendById(postId, comment.CommendtId);
            if (convertedComment == null)
                throw new NotFoundException("Comment not found");

            _postRepository.DeleteCommendtFromPost(convertedComment, foundPost);
        }


        public void DeletePost(int id)
        {
            Post? foundPost = _postRepository.GetPostById(id);
            if (foundPost == null)
                throw new NotFoundException("Post not found");
            _postRepository.DeletePost(id);
        }

    }
}
