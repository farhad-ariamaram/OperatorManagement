using System.Collections.Generic;

namespace OperatorManagementBL.Services
{
    public interface IAuthorizeService
    {
        bool IsAuthorize(string[] Roels, int UserId);

        List<string> GetUserRoles();

        int GetUserId();

        bool checkUserRole(List<string> userRoles, List<string> requiredRoles);
    }
}
