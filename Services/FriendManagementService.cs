using SocailMediaApp.Exceptions;
using SocailMediaApp.Models;
using SocailMediaApp.Repositories;
using SocailMediaApp.ViewModels;

namespace SocailMediaApp.Services
{
    public class FollowingManagementService
    {
        private UserRepository _userRepository;
        public FollowingManagementService(UserRepository _userRepository)
        {
            this._userRepository = _userRepository;
        }

        public void FollowUser(FollowRequestViewModel followRequest)
        {
            int senderId = followRequest.senderId;
            int receiverId = followRequest.receiverId;
            if (senderId == receiverId)
                throw new InvalidOperationException("You cant follow yourself");
            User? senderUser = _userRepository.GetUserById(senderId);
            if (senderUser == null)
                throw new NotFoundException("Sender User not found");
            User? receiverUser = _userRepository.GetUserById(receiverId);
            if (receiverUser == null)
                throw new NotFoundException("Receiver User not found");
            if (_userRepository.IsFollowing(senderUser, receiverUser))
                throw new AlreadyExistesException("Already being followed");
            _userRepository.FollowUser(senderUser, receiverUser);
        }

        public void Unfollow(FollowRequestViewModel followRequest)
        {
            int senderId = followRequest.senderId;
            int receiverId = followRequest.receiverId;
            if (senderId == receiverId)
                throw new InvalidOperationException("You cannot follow yourself");
            User? senderUser = _userRepository.GetUserById(senderId);
            if (senderUser == null)
                throw new NotFoundException("Sender User not found");
            User? receiverUser = _userRepository.GetUserById(receiverId);
            if (receiverUser == null)
                throw new NotFoundException("Receiver User not found");
            if (!_userRepository.IsFollowing(senderUser, receiverUser))
                throw new NotFoundException("You are not following the other user");
            _userRepository.UnfollowUser(senderUser, receiverUser);
        }
        public List<UserFriendViewModel> GetFollowingList(int userId)
        {
            User? foundUser = _userRepository.GetUserById(userId);
            if (foundUser == null)
                throw new NotFoundException("User not found");
            List<User> followingList = _userRepository.GetAllFollowing(userId);
            List<UserFriendViewModel> userFriendViews = new List<UserFriendViewModel>();
            followingList.ForEach(user => {
                UserFriendViewModel userFriendViewModel = new UserFriendViewModel();
                userFriendViewModel.Id = user.Id;
                userFriendViewModel.Name = user.Name;
                userFriendViews.Add(userFriendViewModel);
            });
            return userFriendViews;
        }
        public List<UserFriendViewModel> GetFollowersList(int userId)
        {
            User? foundUser = _userRepository.GetUserById(userId);
            if (foundUser == null)
                throw new NotFoundException("User not found");
            List<User> followingList = _userRepository.GetAllFollowers(userId);
            List<UserFriendViewModel> userFriendViews = new List<UserFriendViewModel>();
            followingList.ForEach(user => {
                UserFriendViewModel userFriendViewModel = new UserFriendViewModel();
                userFriendViewModel.Id = user.Id;
                userFriendViewModel.Name = user.Name;
                userFriendViews.Add(userFriendViewModel);
            });
            return userFriendViews;
        }

    }
}
