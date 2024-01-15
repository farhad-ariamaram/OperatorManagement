
using OperatorManagementBL.Services;
using System;
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

        public ActionResult Index()
        {
            var model = _transactionService.GetTransactions();
            ViewBag.Sim_Id = new SelectList(_simService.GetSims(), "Id", "Number");
            return View(model);
        }

        [HttpPost]
        public JsonResult Test(int fromSimId, int toSimId, int typeId, int duration)
        {
            try
            {
                var res = _transactionService.MakeCall(fromSimId, toSimId, typeId, duration);
                
                if (res == 1)
                {
                    return Json(new { status = false, msg = "Your balance is not Sufficient" });
                }

                return Json(new { status = true });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, msg = "problem on making call" });
            }

        }

    }
}