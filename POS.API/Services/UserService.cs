using Microsoft.EntityFrameworkCore;
using POS.API.Data;
using POS.API.Models;
using POS.API.Models.DTO;

namespace POS.API.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        //for create mail
        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.Trim().ToLower());
        }
        //for update mail .. Business Logic
        public async Task<bool> IsEmailExistsForAnotherUserAsync(
            int id,
            string email)
        {
            return await _context.Users.AnyAsync(
                u => u.Id != id &&
                     u.Email.ToLower() == email.Trim().ToLower());
        }
    
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();

            return users.Select(user => new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Age = user.Age,
                Gender = user.Gender,
                Email = user.Email
            });
        }
        public async Task<UserDTO?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return null;

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Age = user.Age,
                Gender = user.Gender,
                Email = user.Email
            };
        }


        public async Task<UserDTO> CreateUserAsync( CreateUserDTO createUserDTO)
        {
            User user = new()
            {
                Name = createUserDTO.Name,
                Age = createUserDTO.Age,
                Gender = createUserDTO.Gender,
                Email = createUserDTO.Email,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Age = user.Age,
                Gender = user.Gender,
                Email = user.Email
            };
        }

       
        public async Task<UserDTO?> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return null;

            var deletedUser = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Age = user.Age,
                Gender = user.Gender,
                Email = user.Email
            };

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return deletedUser;
        }

     
        public async Task<UserDTO?> UpdateUserAsync( int id, UpdateUserDTO updateUserDTO)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return null;

            user.Name = updateUserDTO.Name;
            user.Age = updateUserDTO.Age;
            user.Gender = updateUserDTO.Gender;
            user.Email = updateUserDTO.Email;

            await _context.SaveChangesAsync();

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Age = user.Age,
                Gender = user.Gender,
                Email = user.Email
            };
        }

    }
}
