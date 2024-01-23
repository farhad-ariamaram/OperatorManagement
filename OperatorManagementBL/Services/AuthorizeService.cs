﻿using OperatorManagementDL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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

            var user = _context.Tbl_User.Find(UserId);

            if (user == null)
            {
                return false;
            }

            List<int> requiredRoles = _context.Tbl_Role
                .Where(r => Roels.Contains(r.Fld_Role_Name))
                .Select(u => u.Fld_Role_Id)
                .ToList();

            var userRoles = user.Tbl_Role.Select(a => a.Fld_Role_Id);

            bool hasUserRoles = requiredRoles
                          .Intersect(userRoles)
                          .Any();

            return hasUserRoles;

        }
    }
}