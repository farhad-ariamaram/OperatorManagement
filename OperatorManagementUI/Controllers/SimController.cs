using OperatorManagementBL.DTOs;
using OperatorManagementBL.Services;
using System;
using System.Web.Mvc;

namespace OperatorManagementUI.Controllers
{
    public class SimController : Controller
    {
        private readonly ISimService _simService;
        private readonly IPersonService _personService;

        public SimController()
        {
            _simService = new SimService();
            _personService = new PersonService();
        }

        public ActionResult Index()
        {
            return View(_simService.GetDetailSims());
        }

        // صفحه جزئیات سیمکارت
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index", "Error", new ErrorDTO { StatusCode = 404 });
                }

                SimDetailDTO sim = _simService.GetSimDetailById(id.Value);

                return View(sim);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new ErrorDTO { Msg = ex.Message, StatusCode = 500 });
            }
        }

        // صفحه ساخت سیمکارت جدید
        // GET
        public ActionResult Create()
        {
            ViewBag.Person_Id = new SelectList(_personService.GetPeopleForDropdown(), "Id", "NameAndNationCode");
            ViewBag.SimType_Id = new SelectList(_simService.GetSimTypes(), "Id", "Type");
            return View();
        }

        // صفحه ساخت سیمکارت جدید
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] SimDTO sim)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _simService.AddSim(sim);
                    return RedirectToAction("Index");
                }

                ViewBag.Person_Id = new SelectList(_personService.GetPeopleForDropdown(), "Id", "NameAndNationCode");
                ViewBag.SimType_Id = new SelectList(_simService.GetSimTypes(), "Id", "Type");
                return View(sim);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new ErrorDTO { Msg = ex.Message, StatusCode = 500 });
            }
        }

        // صفحه ویرایش سیمکارت
        // GET
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index", "Error", new ErrorDTO { StatusCode = 404 });
                }

                SimDTO sim = _simService.GetSimById(id.Value);

                ViewBag.Person_Id = new SelectList(_personService.GetPeopleForDropdown(), "Id", "NameAndNationCode", sim.Person_Id);
                ViewBag.SimType_Id = new SelectList(_simService.GetSimTypes(), "Id", "Type", sim.SimType_Id);
                return View(sim);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new ErrorDTO { Msg = ex.Message, StatusCode = 500 });
            }
        }


        // صفحه ویرایش سیمکارت
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind] SimDTO sim)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _simService.UpdateSim(sim);
                    return RedirectToAction("Index");
                }

                ViewBag.Person_Id = new SelectList(_personService.GetPeopleForDropdown(), "Id", "NameAndNationCode", sim.Person_Id);
                ViewBag.SimType_Id = new SelectList(_simService.GetSimTypes(), "Id", "Type", sim.SimType_Id);

                return View(sim);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new ErrorDTO { Msg = ex.Message, StatusCode = 500 });
            }
        }

        // صفحه حذف سیمکارت
        // GET
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index", "Error", new ErrorDTO { StatusCode = 404 });
                }

                SimDTO sim = _simService.GetSimById(id.Value);

                return View(sim);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Msg = ex.Message });
            }
        }

        // صفحه حذف سیمکارت
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _simService.DeleteSimById(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new ErrorDTO { Msg = ex.Message, StatusCode = 500 });
            }
        }

        // صفحه نمایش سیمکارت های حذف شده
        public ActionResult DeletedSims()
        {
            return View(_simService.GetDeletedSims());
        }

        // صفحه بازگردانی سیمکارت حذف شده
        public ActionResult UnDelete(int id)
        {
            try
            {
                _simService.UnDeleteSimById(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new ErrorDTO { Msg = ex.Message, StatusCode = 500 });
            }
        }

        // صفحه شارژ/پرداخت قبض سیمکارت
        // GET
        public ActionResult Wallet(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index", "Error", new ErrorDTO { StatusCode = 500 });
                }

                var currentsim = _simService.GetWallet(id.Value);

                ViewBag.wallet = currentsim;

                var walletChargeDTO = new WalletChargeDTO
                {
                    SimId = id.Value,
                    AddBalance = 0,
                    SimTypeId = currentsim.SimTypeId
                };

                return View(walletChargeDTO);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new ErrorDTO { Msg = ex.Message, StatusCode = 500 });
            }
        }

        // صفحه شارژ/پرداخت قبض سیمکارت
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Wallet([Bind] WalletChargeDTO walletChargeDTO)
        {
            try
            {
                switch (walletChargeDTO.SimTypeId)
                {
                    //شارژ سیمکارت اعتباری
                    case (int)OperatorManagementBL.Enum.SimTypeEnum.credit:
                        {
                            // اگر مبلغ شارژ معتبر نبود خطا
                            if (walletChargeDTO.AddBalance <= 0)
                            {
                                ModelState.AddModelError("InvalidBalance", "مبلغ شارژ معتبر نیست");
                                ViewBag.wallet = _simService.GetWallet(walletChargeDTO.SimId);
                                return View(walletChargeDTO);
                            }

                            //انجام شارژ
                            _simService.ChargeSim(walletChargeDTO.SimId, walletChargeDTO.AddBalance);

                            //فلگ موفق بودن شارژ
                            ViewBag.ChargeSuccess = true;

                            break;
                        }
                    //پرداخت قبض سیمکارت دائمی
                    case (int)OperatorManagementBL.Enum.SimTypeEnum.permanent:
                        {
                            //انجام پرداخت قبض
                            _simService.PayBillSim(walletChargeDTO.SimId);

                            //فلگ موفق بودن پرداخت قبض
                            ViewBag.PayBillSuccess = true;

                            break;
                        }
                    default:
                        break;
                }

                ViewBag.wallet = _simService.GetWallet(walletChargeDTO.SimId);

                return View(walletChargeDTO);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new ErrorDTO { Msg = ex.Message, StatusCode = 500 });
            }
        }

        public ActionResult ChargeLog(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index", "Error", new ErrorDTO { StatusCode = 404 });
                }

                return View(_simService.GetChargeLogs(id.Value));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new ErrorDTO { Msg = ex.Message, StatusCode = 500 });
            }
        }
    }
}
