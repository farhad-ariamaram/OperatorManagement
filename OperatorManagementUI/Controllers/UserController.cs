using OperatorManagementBL.Attributes;
using OperatorManagementBL.DTOs;
using OperatorManagementBL.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OperatorManagementUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPersonService _personService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IAuthorizeService _authorizeService;

        public UserController()
        {
            _userService = new UserService();
            _personService = new PersonService();
            _authenticationService = new AuthenticationService();
            _authorizeService = new AuthorizeService();
        }

        [MyAuthenticate]
        public async Task<ActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        [MyAuthenticate]
        public async Task<ActionResult> Details(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return View(user);
        }

        [MyAuthorize(Roles = "Admin,AddEditUser")]
        public async Task<ActionResult> CreateEdit(int? id)
        {

            if (id.HasValue)
            {
                // ویرایش کاربر
                var user = await _userService.GetUserByIdAsync(id.Value);
                if (user == null)
                {
                    return HttpNotFound();
                }

                ViewBag.PersonId = new SelectList(_personService.GetPeopleForDropdown(), "Id", "NameAndNationCode", user.PersonId);
                return View(user);
            }

            // ساخت کاربر جدید
            ViewBag.PersonId = new SelectList(_personService.GetPeopleForDropdown(), "Id", "NameAndNationCode");
            return View(new UserDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAuthorize(Roles = "Admin,AddEditUser")]
        public async Task<ActionResult> CreateEdit(UserDTO user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _userService.CreateOrUpdateUserAsync(user);
                    return RedirectToAction("Index");
                }

                ViewBag.PersonId = new SelectList(_personService.GetPeopleForDropdown(), "Id", "NameAndNationCode", user.PersonId);
                return View(user);
            }
            catch (System.Exception ex)
            {
                return RedirectToAction("Index", "Error", new ErrorDTO { Msg = ex.Message, StatusCode = 500 });
            }
        }

        [MyAuthorize(Roles = "Admin,LockUser")]
        public async Task<ActionResult> Lock(int id)
        {
            await _userService.LockUserAsync(id);
            return RedirectToAction("Index");
        }

        [MyAuthorize(Roles = "Admin,UnLockUser")]
        public async Task<ActionResult> UnLock(int id)
        {
            await _userService.UnLockUserAsync(id);
            return RedirectToAction("Index");
        }

        public ActionResult Login(string redirectUrl)
        {
            ViewBag.Redirected = !string.IsNullOrEmpty(redirectUrl) ? true : false;

            if (Session["UserId"] != null)
            {
                return Redirect("Index");
            }

            var viewModel = new UserLoginDTO();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([Bind] UserLoginDTO loginDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = await _authenticationService.AuthenticateUserAsync(loginDTO);

                    Session["UserId"] = userId;

                    return Redirect("Index");
                }

                ViewBag.Redirected = false;
                return View(loginDTO);
            }
            catch
            {
                ViewBag.Redirected = false;
                ModelState.AddModelError("WrongUserOrPass", "نام کاربری و یا کلمه عبور اشتباه است");
                return View(loginDTO);
            }
        }


        [MyAuthorize(Roles = "Admin,EditRoles")]
        public async Task<ActionResult> EditRoles(int id)
        {
            var userRoles = await _userService.GetUserRoles(id);
            return View(userRoles);
        }

        [HttpPost]
        [MyAuthorize(Roles = "Admin,EditRoles")]
        public async Task<ActionResult> EditRoles(int id, List<UserRolesDTO> userRolesDto)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateUserRoles(id, userRolesDto);
                return RedirectToAction("Index");
            }

            return View(userRolesDto);
        }

        public ActionResult Logout()
        {
            Session["UserId"] = null;
            return RedirectToAction("Login");
        }
    }

}