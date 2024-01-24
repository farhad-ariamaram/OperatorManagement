using OperatorManagementDL;
using System.Collections.Generic;
using System.Linq;

namespace OperatorManagementBL.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly OperatorManagementDBEntities _context;

        public AuthorizeService()
        {
            _context = new OperatorManagementDBEntities();
        }

        public bool IsAuthorize(string[] Roels, int UserId)
        {
            if (UserId == 0)
            {
                return false;
            }

            var user = _context.Tbl_User.AsNoTracking().SingleOrDefault(a => a.Fld_User_Id == UserId);

            if (user == null)
            {
                return false;
            }

            List<int> requiredRoles = _context.Tbl_Role.AsNoTracking()
                .Where(r => Roels.Contains(r.Fld_Role_Name))
                .Select(u => u.Fld_Role_Id)
                .ToList();

            var userRoles = user.Tbl_Role.Select(a => a.Fld_Role_Id);

            bool hasUserRoles = requiredRoles
                          .Intersect(userRoles)
                          .Any();

            return hasUserRoles;

        }

        public int GetUserId()
        {
            var userId = System.Web.HttpContext.Current.Session["UserId"];
            if (userId == null || (int)userId == 0)
            {
                return 0;
            }

            return (int)userId;
        }

        public List<string> GetUserRoles()
        {
            var userId = System.Web.HttpContext.Current.Session["UserId"];
            if (userId == null || (int)userId == 0)
            {
                return null;
            }

            var user =  _context.Tbl_User.AsNoTracking().SingleOrDefault(a => a.Fld_User_Id == (int)userId);
            if (user == null)
            {
                return null;
            }

            return user.Tbl_Role.Select(a=>a.Fld_Role_Name).ToList();
        }

        public bool checkUserRole(List<string> userRoles, List<string> requiredRoles)
        {
            return requiredRoles
              .Intersect(userRoles)
              .Any();
        }
    }
}
