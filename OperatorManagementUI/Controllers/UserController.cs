using OperatorManagementBL.DTOs;
using OperatorManagementBL.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OperatorManagementUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPersonService _personService;
        private readonly IAuthenticationService _authenticationService;

        public UserController()
        {
            _userService = new UserService();
            _personService = new PersonService();
            _authenticationService = new AuthenticationService();
        }

        public async Task<ActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        public async Task<ActionResult> Details(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return View(user);
        }


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

                ViewBag.PersonId = new SelectList(_personService.GetPeopleForDropdown(), "Id", "NameAndNationCode",user.PersonId);
                return View(user);
            }

            // ساخت کاربر جدید
            ViewBag.PersonId = new SelectList(_personService.GetPeopleForDropdown(), "Id", "NameAndNationCode");
            return View(new UserDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public async Task<ActionResult> Lock(int id)
        {
            await _userService.LockUserAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> UnLock(int id)
        {
            await _userService.UnLockUserAsync(id);
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
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

                return View(loginDTO);
            }
            catch (System.Exception ex)
            {
                return RedirectToAction("Index", "Error", new ErrorDTO { Msg = ex.Message, StatusCode = 500 });
            }
        }
    }

}