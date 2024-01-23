using System.Threading.Tasks;

namespace OperatorManagementBL.Services
{
    public interface IAuthorizeService
    {
        bool IsAuthorize(string[] Roels, int UserId);
    }
}
