using SocailMediaApp.Exceptions;
using SocailMediaApp.Models;
using SocailMediaApp.Repositories;
using SocailMediaApp.ViewModels;
using System.Net.Mail;


namespace SocailMediaApp.Services
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserFriendViewModel> GetAllUsers()
        {
            List<User> users = _userRepository.GetAllUsers();
            //convert users to user friend view models
            List<UserFriendViewModel> userFriendViewModels = new List<UserFriendViewModel>();
            foreach (User user in users)
            {
                UserFriendViewModel userFriendViewModel = new UserFriendViewModel
                {
                    Id = user.Id,
                    Name = user.Name
                };
                userFriendViewModels.Add(userFriendViewModel);
            }
            return userFriendViewModels;
        }

        public void Register(RegisterUserViewModel user,HttpRequest httpRequest)
        {
            User? foundUser = _userRepository.GetUserByEmail(user.Email);
            if (foundUser != null)
            {
                throw new AlreadyExistesException("Email already exists.");
            }

            User convertedUser = new User
            {
                Email = user.Email,
                Name = user.Name,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Address = user.Address,
                Phone = user.Phone
            };

            _userRepository.AddUser(convertedUser);
            try
            {
                SendConfirmationEmail(convertedUser, httpRequest);
            }
            catch (Exception e)
            {
                _userRepository.DeleteUser(convertedUser);
                throw new MailConfirmationException(e.Message);
            }

        }
        private void SendConfirmationEmail(User user,HttpRequest httpRequest)
        {
            String verificationLink = httpRequest.Scheme + "://" + httpRequest.Host + "/api/v1/verify/" + user.Id;

            string htmlBody = $@"
        <html>
        <body>
            <h1>Complete Your Registration</h1>
            <p>Click the button below to complete your registration:</p>
            <a href='{verificationLink}' style='background-color: blue; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Enter here to register</a>
        </body>
        </html>";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("mohanadkhaled87@gmail.com");
                mail.To.Add(user.Email);
                mail.Subject = "Complete Your Registration";
                mail.Body = htmlBody;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    String username = "";
                    String password = "";
                    smtp.Credentials = new System.Net.NetworkCredential(username, password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }

        public string Login(LoginUserViewModel user)
        {

                User? foundUser = _userRepository.GetUserByEmail(user.Email);
                if (foundUser == null)
                {
                    throw new NotFoundException("Email not found!");
                }

                bool matchingPassword = BCrypt.Net.BCrypt.Verify(user.Password, foundUser.Password);
                if (!matchingPassword)
                {
                    throw new InvalidException("Wrong Password!");
                }

                if (!foundUser.EmailConfirmed)
                {
                    throw new InvalidException("Email not confirmed!");
                }

                return "TOKEN";
           
        }

        public void Verify(int id)
        {
 
                User? foundUser = _userRepository.GetUserById(id);
                if (foundUser == null)
                {
                    throw new KeyNotFoundException("User not found!");
                }

                if (foundUser.EmailConfirmed)
                {
                    throw new InvalidOperationException("Email already confirmed!");
                }

                foundUser.EmailConfirmed = true;
                _userRepository.UpdateUserConfirmation(foundUser); 
            

        }
    }
}
