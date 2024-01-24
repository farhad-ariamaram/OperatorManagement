using OperatorManagementBL.DTOs;
using OperatorManagementBL.Extensions;
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
        private readonly IAuthorizeService _authorizeService;

        public UserService()
        {
            _context = new OperatorManagementDBEntities();
            _authorizeService = new AuthorizeService();
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var userRoles = _authorizeService.GetUserRoles();
            var requiredRoles = new List<string> { "Admin", "ViewUsers" };

            IQueryable<Tbl_User> people = _context.Tbl_User;
            if (!_authorizeService.checkUserRole(userRoles, requiredRoles))
            {
                var logonUserId = _authorizeService.GetUserId();
                people = people.Where(a => a.Fld_User_Id == logonUserId);
            }

            var users = await people
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
            var logonUserId = _authorizeService.GetUserId();

            if (logonUserId != userId)
            {
                var userRoles = _authorizeService.GetUserRoles();
                var requiredRoles = new List<string> { "Admin", "ViewUserDetail" };
                if (!_authorizeService.checkUserRole(userRoles, requiredRoles))
                {
                    throw new System.Exception("شما اجازه دیدن مشخصات این کاربر را ندارید");

                }
            }

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
            //چک کردن نام کاربری تکراری
            var cuser = _context.Tbl_User.Where(a => a.Fld_User_Username == user.Username || a.Fld_User_Email == user.Email);

            var hashPassword = user.Password.ToSha256Hash();
            if (user.UserId == 0)
            {
                if (cuser.Any())
                {
                    throw new System.Exception("نام کاربری یا ایمیل تکراری است");
                }

                var newUser = new Tbl_User
                {
                    Fld_User_Username = user.Username,
                    Fld_User_Email = user.Email,
                    Fld_User_IsLocked = user.IsLocked,
                    Fld_User_Password = hashPassword,
                    Fld_Person_PersonId = user.PersonId
                };

                _context.Tbl_User.Add(newUser);
            }
            else
            {
                if (cuser.Where(a => a.Fld_User_Id != user.UserId).Any())
                {
                    throw new System.Exception("نام کاربری یا ایمیل تکراری است");
                }

                var existingUser = await _context.Tbl_User.FindAsync(user.UserId);
                if (existingUser != null)
                {
                    existingUser.Fld_User_Username = user.Username;
                    existingUser.Fld_User_Email = user.Email;
                    existingUser.Fld_User_IsLocked = user.IsLocked;
                    existingUser.Fld_User_Password = hashPassword;
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

        public async Task<List<UserRolesDTO>> GetUserRoles(int userId)
        {
            var user = await _context.Tbl_User.FindAsync(userId);
            if (user != null)
            {
                var mappedUserRoles = new List<UserRolesDTO>();
                var userRoles = user.Tbl_Role;
                var roles = _context.Tbl_Role;
                foreach (var item in roles)
                {
                    if (userRoles.Contains(item))
                    {
                        mappedUserRoles.Add(new UserRolesDTO
                        {
                            RoleId = item.Fld_Role_Id,
                            RoleName = item.Fld_Role_Name,
                            IsActive = true
                        });
                    }
                    else
                    {
                        mappedUserRoles.Add(new UserRolesDTO
                        {
                            RoleId = item.Fld_Role_Id,
                            RoleName = item.Fld_Role_Name,
                            IsActive = false
                        });
                    }
                }
                return mappedUserRoles;
            }
            else
            {
                throw new System.Exception("کاربر پیدا نشد");
            }
        }

        public async Task UpdateUserRoles(int userId, List<UserRolesDTO> userRolesDto)
        {
            var user = await _context.Tbl_User.FindAsync(userId);
            if (user != null)
            {
                var userRoles = user.Tbl_Role;
                foreach (var item in userRolesDto)
                {
                    var role = await _context.Tbl_Role.FindAsync(item.RoleId);
                    if (item.IsActive)
                    {
                        if (!userRoles.Contains(role))
                        {
                            userRoles.Add(role);
                        }
                    }
                    else
                    {
                        if (userRoles.Contains(role))
                        {
                            userRoles.Remove(role);
                        }
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

    }

}
