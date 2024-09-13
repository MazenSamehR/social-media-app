using SocailMediaApp.Controllers;
using SocailMediaApp.Exceptions;
using SocailMediaApp.Models;

namespace SocailMediaApp.Repositories
{
    public class UserRepository
    {
        public List<User> _users;
        public UserRepository(List<User> users)
        {
            _users = users;
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public User? GetUserByEmail (string email)
        {

            return _users.FirstOrDefault(u => u.Email.Equals(email));
        }
        public User? GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.Id.Equals(id));
        }

        public List<User> GetAllFollowing(int userId)
        {
            User? user = GetUserById(userId);
            if (user == null)
                throw new NotFoundException("User not found");
            return user.Following;
        }
        public List<User> GetAllFollowers(int userId)
        {
            User? user = GetUserById(userId);
            if (user == null)
                throw new NotFoundException("User not found");
            return user.Followers;
        }

        public bool IsFollowing(User sender,User receiver)
        {
            return sender.Following.Find(following => following.Equals(receiver)) != null;
        }

        public void AddUser(User user)
        {
            user.Id = _users.Count + 1;
            _users.Add(user);
        }

        public void FollowUser(User sender,User receiver)
        {
            sender.Following.Add(receiver);
            receiver.Followers.Add(sender);

            // TODO : save users info into the database instead of the in-memory list
        }
        public void UnfollowUser(User sender, User receiver)
        {
            sender.Following.Remove(receiver);
            receiver.Followers.Remove(sender);

            // TODO : save users info into the database instead of the in-memory list
        }

        public void UpdateUserConfirmation(User user)
        {
            User? foundUser = GetUserById(user.Id);
            if (foundUser != null)
            {
                foundUser.EmailConfirmed = true;
            }
        }
        
        public void UpdateUser(int userId, User user)
        {
            User? foundUser = GetUserById(userId);
            if (foundUser == null)
            {
                throw new NotFoundException("User not found!");
            }
            foundUser.Name = user.Name;
            foundUser.Email = user.Email;
            foundUser.Password = user.Password;
            foundUser.ProfileImageUrl = user.ProfileImageUrl;
            foundUser.EmailConfirmed = user.EmailConfirmed;
            foundUser.Phone = user.Phone;
            foundUser.Address = user.Address;
            // TODO : save users info into the database instead of the in-memory list

        }

        public void DeleteUser(User user)
        {
            _users.Remove(user);
        }
        public void DeleteUser(int userId)
        {
            User? user = GetUserById(userId);
            if (user == null)
            {
                throw new NotFoundException("User not found!");
            }
            _users.Remove(user);
        }


    }
}
