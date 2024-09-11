using SocailMediaApp.Controllers;
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
            /*user.Id = _users.Count + 1;*/
            _users.Add(user);
        }

        //there's an UpdateUser method to save changes for verify function 
        public void UpdateUser(User user)
        {
            User? foundUser = GetUserById(user.Id);
            if (foundUser != null)
            {
                foundUser.EmailConfirmed = true;
            }
        }




    }
}
