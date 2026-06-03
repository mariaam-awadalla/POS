using POS.API.Models.DTO;

namespace POS.API.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();

        Task<UserDTO?> GetUserByIdAsync(int id);

        Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDTO);

        Task<UserDTO?> UpdateUserAsync(int id, UpdateUserDTO updateUserDTO);

        Task<UserDTO?> DeleteUserAsync(int id);

        Task<bool> IsEmailExistsAsync(string email);
        Task<bool> IsEmailExistsForAnotherUserAsync(int id,string email);
    }
}
