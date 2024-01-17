﻿
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
            ViewBag.Sim_Id = new SelectList(_simService.GetSims(), "Id", "Number");
            return View();
        }

        public ActionResult Transactions(DateTime? fromDate, DateTime? toDate, int fromSimId = 0, int toSimId = 0, int fromPersonId = 0, int toPersonId = 0, int durationLessThan = 0, int durationMoreThan = 0, int typeId = 0)
        {
            var vm = _transactionService.GetTransactions(fromDate ?? null, toDate ?? null, fromSimId, toSimId, fromPersonId, toPersonId, durationLessThan, durationMoreThan, typeId);
            return View(vm);
        }

        #region AJAX_METHODS
        [HttpPost]
        public JsonResult MakeCall(int fromSimId, int toSimId, int typeId, int duration)
        {
            var res = _transactionService.MakeCall(fromSimId, toSimId, typeId, duration);
            return Json(new { statusCode = res });
        }

        [HttpPost]
        public JsonResult SendSMS(int fromSimId, int toSimId, int typeId)
        {
            var res = _transactionService.SendSMS(fromSimId, toSimId, typeId);
            return Json(new { statusCode = res });
        }

        [HttpPost]
        public JsonResult GetSimsForDropdown(int fromSimId)
        {
            var res = _simService.GetSimsForDropdown(fromSimId);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}