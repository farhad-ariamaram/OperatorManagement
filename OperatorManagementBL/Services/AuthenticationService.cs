using OperatorManagementBL.DTOs;
using OperatorManagementBL.Exceptions;
using OperatorManagementBL.Extensions;
using OperatorManagementDL;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace OperatorManagementBL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly OperatorManagementDBEntities _context;

        public AuthenticationService()
        {
            _context = new OperatorManagementDBEntities();
        }

        public async Task<int> AuthenticateUserAsync(UserLoginDTO loginDTO)
        {

            var hashPassword = loginDTO.Password.ToSha256Hash();
            var user = await _context.Tbl_User.SingleOrDefaultAsync(u => u.Fld_User_Username == loginDTO.Username && u.Fld_User_Password == hashPassword);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (user.Fld_User_IsLocked)
            {
                throw new UserLockedException();
            }

            _context.Tbl_UserSession.Add(new Tbl_UserSession { 
                Fld_UserSession_DateTime = DateTime.Now,
                Fld_User_UserId = user.Fld_User_Id
            });

            await _context.SaveChangesAsync();
 
            return user.Fld_User_Id;

        }
    }
}
