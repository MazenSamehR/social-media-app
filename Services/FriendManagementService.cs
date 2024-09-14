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

        public async Task FollowUser(FollowRequestViewModel followRequest)
        {
            int senderId = followRequest.senderId;
            int receiverId = followRequest.receiverId;
            if (senderId == receiverId)
                throw new InvalidOperationException("You cant follow yourself");
            User? senderUser = await _userRepository.GetUserById(senderId);
            if (senderUser == null)
                throw new NotFoundException("Sender User not found");
            User? receiverUser = await _userRepository.GetUserById(receiverId);
            if (receiverUser == null)
                throw new NotFoundException("Receiver User not found");
            if (_userRepository.IsFollowing(senderUser, receiverUser))
                throw new AlreadyExistesException("Already being followed");
            await _userRepository.FollowUser(senderUser, receiverUser);
        }

        public async Task Unfollow(FollowRequestViewModel followRequest)
        {
            int senderId = followRequest.senderId;
            int receiverId = followRequest.receiverId;
            if (senderId == receiverId)
                throw new InvalidOperationException("You cannot follow yourself");
            User? senderUser = await _userRepository.GetUserById(senderId);
            if (senderUser == null)
                throw new NotFoundException("Sender User not found");
            User? receiverUser = await _userRepository.GetUserById(receiverId);
            if (receiverUser == null)
                throw new NotFoundException("Receiver User not found");
            if (!_userRepository.IsFollowing(senderUser, receiverUser))
                throw new NotFoundException("You are not following the other user");
            await _userRepository.UnfollowUser(senderUser, receiverUser);
        }
        public async Task<List<UserFriendViewModel>> GetFollowingList(int userId)
        {
            User? foundUser = await _userRepository.GetUserById(userId);
            if (foundUser == null)
                throw new NotFoundException("User not found");
            List<User> followingList = await _userRepository.GetAllFollowing(userId);
            List<UserFriendViewModel> userFriendViews = new List<UserFriendViewModel>();
            followingList.ForEach(user => {
                UserFriendViewModel userFriendViewModel = new UserFriendViewModel();
                userFriendViewModel.Id = user.Id;
                userFriendViewModel.Name = user.Name;
                userFriendViewModel.ProfileImageUrl = user.ProfileImageUrl;
                userFriendViews.Add(userFriendViewModel);
            });
            return userFriendViews;
        }
        public async Task<List<UserFriendViewModel>> GetFollowersList(int userId)
        {
            User? foundUser = await _userRepository.GetUserById(userId);
            if (foundUser == null)
                throw new NotFoundException("User not found");
            List<User> followingList = await _userRepository.GetAllFollowers(userId);
            List<UserFriendViewModel> userFriendViews = new List<UserFriendViewModel>();
            followingList.ForEach(user => {
                UserFriendViewModel userFriendViewModel = new UserFriendViewModel();
                userFriendViewModel.Id = user.Id;
                userFriendViewModel.Name = user.Name;
                userFriendViewModel.ProfileImageUrl = user.ProfileImageUrl;
                userFriendViews.Add(userFriendViewModel);
            });
            return userFriendViews;
        }

    }
}
