using OperatorManagementBL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OperatorManagementBL.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int userId);
        Task<int> CreateOrUpdateUserAsync(UserDTO user);
        Task LockUserAsync(int userId);
        Task UnLockUserAsync(int userId);
    }
}
