using SocailMediaApp.Controllers;
using SocailMediaApp.Exceptions;
using SocailMediaApp.Models;

namespace SocailMediaApp.Repositories
{
    public class UserRepository
    {
        public static List<User> _users = new List<User>();

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

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public void UpdateUserConfirmation(User user)
        {
            User? foundUser = GetUserById(user.Id);
            if (foundUser != null)
            {
                foundUser.EmailConfirmed = true;
            }
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
