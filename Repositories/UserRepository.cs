using SocailMediaApp.Controllers;
using SocailMediaApp.Exceptions;
using SocailMediaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace SocailMediaApp.Repositories
{
    public class UserRepository
    {
        private readonly SocialMediaContext _context;

        public UserRepository(SocialMediaContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.Select(u => new User
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                ProfileImageUrl = u.ProfileImageUrl,
                EmailConfirmed = u.EmailConfirmed,
                Phone = u.Phone,
                Address = u.Address
            }).ToListAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllFollowing(int userId)
        {
            var user = await GetUserById(userId);
            if (user == null)
                throw new NotFoundException("User not found");

            // Return the list of Users that the user is following
            return await _context.UserFollowers
                .Where(uf => uf.FollowerId == userId)
                .Select(uf => uf.Followed) // Fetch the actual User entity
                .ToListAsync();
        }

        public async Task<List<User>> GetAllFollowers(int userId)
        {
            var user = await GetUserById(userId);
            if (user == null)
                throw new NotFoundException("User not found");

            // Return the list of Users that are following the user
            return await _context.UserFollowers
                .Where(uf => uf.FollowedId == userId)
                .Select(uf => uf.Follower) // Fetch the actual User entity
                .ToListAsync();
        }


        public bool IsFollowing(User sender, User receiver)
        {
            return _context.UserFollowers.Any(uf => uf.FollowerId == sender.Id && uf.FollowedId == receiver.Id);
        }

        public async Task FollowUser(User sender, User receiver)
        {
            var userFollower = new UserFollower
            {
                FollowerId = sender.Id,
                FollowedId = receiver.Id
            };

            _context.UserFollowers.Add(userFollower);
            await _context.SaveChangesAsync();
        }

        public async Task UnfollowUser(User sender, User receiver)
        {
            var userFollower = await _context.UserFollowers
                .FirstOrDefaultAsync(uf => uf.FollowerId == sender.Id && uf.FollowedId == receiver.Id);

            if (userFollower != null)
            {
                _context.UserFollowers.Remove(userFollower);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserConfirmation(User user)
        {
            var foundUser = await GetUserById(user.Id);
            if (foundUser != null)
            {
                foundUser.EmailConfirmed = true;
                _context.Users.Update(foundUser);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateUser(int userId, User user)
        {
            var foundUser = await GetUserById(userId);
            if (foundUser == null)
                throw new NotFoundException("User not found!");

            foundUser.Name = user.Name;
            foundUser.Email = user.Email;
            foundUser.Password = user.Password;
            foundUser.ProfileImageUrl = user.ProfileImageUrl;
            foundUser.EmailConfirmed = user.EmailConfirmed;
            foundUser.Phone = user.Phone;
            foundUser.Address = user.Address;

            _context.Users.Update(foundUser);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int userId)
        {
            var user = await GetUserById(userId);
            if (user == null)
                throw new NotFoundException("User not found!");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
