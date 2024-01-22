using OperatorManagementBL.DTOs;
using OperatorManagementDL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OperatorManagementBL.Services
{
    public class UserService : IUserService
    {
        private readonly OperatorManagementDBEntities _context;

        public UserService()
        {
            _context = new OperatorManagementDBEntities();
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _context.Tbl_User
                .Select(u => new UserDTO
                {
                    UserId = u.Fld_User_Id,
                    Username = u.Fld_User_Username,
                    Email = u.Fld_User_Email,
                    IsLocked = u.Fld_User_IsLocked,
                    Password = u.Fld_User_Password,
                    PersonId = u.Fld_Person_PersonId,
                    Person = u.Tbl_Person.Fld_Person_Fname + " " + u.Tbl_Person.Fld_Person_Lname + "(" + u.Tbl_Person.Fld_Person_NationCode + ")"
                })
                .ToListAsync();

            return users;
        }

        public async Task<UserDTO> GetUserByIdAsync(int userId)
        {
            var user = await _context.Tbl_User
                .Where(u => u.Fld_User_Id == userId)
                .Select(u => new UserDTO
                {
                    UserId = u.Fld_User_Id,
                    Username = u.Fld_User_Username,
                    Email = u.Fld_User_Email,
                    IsLocked = u.Fld_User_IsLocked,
                    Password = u.Fld_User_Password,
                    PersonId = u.Fld_Person_PersonId
                })
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<int> CreateOrUpdateUserAsync(UserDTO user)
        {
            if (user.UserId == 0)
            {
                var newUser = new Tbl_User
                {
                    Fld_User_Username = user.Username,
                    Fld_User_Email = user.Email,
                    Fld_User_IsLocked = user.IsLocked,
                    Fld_User_Password = user.Password,
                    Fld_Person_PersonId = user.PersonId
                };

                _context.Tbl_User.Add(newUser);
            }
            else
            {
                var existingUser = await _context.Tbl_User.FindAsync(user.UserId);
                if (existingUser != null)
                {
                    existingUser.Fld_User_Username = user.Username;
                    existingUser.Fld_User_Email = user.Email;
                    existingUser.Fld_User_IsLocked = user.IsLocked;
                    existingUser.Fld_User_Password = user.Password;
                    existingUser.Fld_Person_PersonId = user.PersonId;
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task LockUserAsync(int userId)
        {
            var user = await _context.Tbl_User.FindAsync(userId);
            if (user != null)
            {
                user.Fld_User_IsLocked = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UnLockUserAsync(int userId)
        {
            var user = await _context.Tbl_User.FindAsync(userId);
            if (user != null)
            {
                user.Fld_User_IsLocked = false;
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            throw new System.Exception();
        }

        public async Task<bool> AuthorizeAsync(string username, string role)
        {
            throw new System.Exception();
        }
    }

}
