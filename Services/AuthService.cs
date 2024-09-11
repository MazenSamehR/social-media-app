using Microsoft.AspNetCore.Http.HttpResults;
using SocailMediaApp.Exceptions;
using SocailMediaApp.Models;
using SocailMediaApp.Repositories;
using SocailMediaApp.Utils;
using SocailMediaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Authentication;

namespace SocailMediaApp.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepository = new UserRepository();

        public List<User> GetAllUsers()
        {
            try
            {
                return _userRepository.GetAllUsers();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching users.", ex);
            }
        }

        public void Register(RegisterUserViewModel user)
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

                // TODO: Send email confirmation
            
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
                _userRepository.UpdateUser(foundUser); 
            

        }
    }
}
