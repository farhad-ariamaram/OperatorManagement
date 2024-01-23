using OperatorManagementBL.DTOs;
using OperatorManagementDL;
using System.Threading.Tasks;

namespace OperatorManagementBL.Services
{
    public interface IAuthenticationService
    {
        Task<int> AuthenticateUserAsync(UserLoginDTO loginDTO);
    }
}
