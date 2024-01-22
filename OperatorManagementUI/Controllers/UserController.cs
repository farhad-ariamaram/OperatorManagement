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

        public UserController()
        {
            _userService = new UserService();
            _personService = new PersonService();
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
                // Edit existing user
                var user = await _userService.GetUserByIdAsync(id.Value);
                if (user == null)
                {
                    return HttpNotFound();
                }

                ViewBag.PersonId = new SelectList(_personService.GetPeopleForDropdown(), "Id", "NameAndNationCode",user.PersonId);
                return View(user);
            }

            // Create new user
            ViewBag.PersonId = new SelectList(_personService.GetPeopleForDropdown(), "Id", "NameAndNationCode");
            return View(new UserDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateEdit(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                await _userService.CreateOrUpdateUserAsync(user);
                return RedirectToAction("Index");
            }

            ViewBag.PersonId = new SelectList(_personService.GetPeopleForDropdown(), "Id", "NameAndNationCode", user.PersonId);
            return View(user);
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
    }

}