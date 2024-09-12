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
            ReadPostViewModel postViewModel = new ReadPostViewModel
            {
                Content = foundPost.Content,
                UserId = foundPost.UserId
            };
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
                ReadPostViewModel postViewModel = new ReadPostViewModel
                {
                    Content = post.Content,
                    UserId = post.UserId
                };
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
                ReadPostViewModel postViewModel = new ReadPostViewModel
                {
                    Content = post.Content,
                    UserId = post.UserId
                };
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
        //delete post by id
        public void DeletePost(int id)
        {
            Post? foundPost = _postRepository.GetPostById(id);
            if (foundPost == null)
                throw new NotFoundException("Post not found");
            _postRepository.DeletePost(id);
        }

    }
}
