
using OperatorManagementBL.Services;
using System.Web.Mvc;

namespace OperatorManagementUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IPersonService _personService;
        private readonly ISimService _simService;

        public HomeController()
        {
            _transactionService = new TransactionService();
            _personService = new PersonService();
            _simService = new SimService();
        }

        // صفحه اصلی سایت شامل دو قسمت برای انجام تماس و فرستادن پیامک
        public ActionResult Index()
        {
            ViewBag.Sim_Id = new SelectList(_simService.GetSims(), "Id", "Number");
            return View();
        }

        // صفحه نمایش تراکنش های انجام شده
        public ActionResult Transactions(int pageId = 1, long fromDate = 0, long toDate = 0, int fromSimId = 0, int toSimId = 0, int fromPersonId = 0, int toPersonId = 0, int durationLessThan = 0, int durationMoreThan = 0, int typeId = 0, int sortType = 0, string search = "")
        {
            ViewBag.Person_Id = new SelectList(_personService.GetPeopleForDropdown(), "Id", "NameAndNationCode");
            ViewBag.Sim_Id = new SelectList(_simService.GetSimsForDropdown(0), "Id", "Number");

            var vm = _transactionService.GetTransactions(pageId, fromDate, toDate, fromSimId, toSimId, fromPersonId, toPersonId, durationLessThan, durationMoreThan, typeId, sortType, search);
            return View(vm);
        }

        // متد های AJAX استفاده شده
        #region AJAX_METHODS
        
        //متد برقراری تماس
        [HttpPost]
        public JsonResult MakeCall(int fromSimId, int toSimId, int typeId, int duration)
        {
            var res = _transactionService.MakeCall(fromSimId, toSimId, typeId, duration);
            return Json(new { statusCode = res });
        }

        //متد ارسال پیامک
        [HttpPost]
        public JsonResult SendSMS(int fromSimId, int toSimId, int typeId)
        {
            var res = _transactionService.SendSMS(fromSimId, toSimId, typeId);
            return Json(new { statusCode = res });
        }

        //متد گرفتن لیست سیمکارت ها برای دراپ دان
        [HttpPost]
        public JsonResult GetSimsForDropdown(int fromSimId)
        {
            var res = _simService.GetSimsForDropdown(fromSimId);
            return Json(res);
        }
        #endregion

    }
}