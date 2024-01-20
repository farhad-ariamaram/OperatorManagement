using OperatorManagementBL.DTOs;
using OperatorManagementBL.Services;
using System;
using System.Net;
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

        /// <summary>
        /// صفحه جزئیات سیمکارت
        /// </summary>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SimDetailDTO sim = _simService.GetSimDetailById(id.Value);
                if (sim == null)
                {
                    return HttpNotFound();
                }
                return View(sim);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Msg = ex.Message });
            }
        }

        /// <summary>
        /// صفحه ساخت سیمکارت جدید
        /// GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Person_Id = new SelectList(_personService.GetPeopleForDropdown(), "Id", "NameAndNationCode");
            ViewBag.SimType_Id = new SelectList(_simService.GetSimTypes(), "Id", "Type");
            return View();
        }


        /// <summary>
        /// صفحه ساخت سیمکارت جدید
        /// POST
        /// </summary>
        /// <returns></returns>
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
                return RedirectToAction("Index", "Error", new { Msg = ex.Message });
            }
        }

        /// <summary>
        /// صفحه ویرایش سیمکارت
        /// GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SimDTO sim = _simService.GetSimById(id.Value);
                if (sim == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Person_Id = new SelectList(_personService.GetPeopleForDropdown(), "Id", "NameAndNationCode", sim.Person_Id);
                ViewBag.SimType_Id = new SelectList(_simService.GetSimTypes(), "Id", "Type", sim.SimType_Id);
                return View(sim);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Msg = ex.Message });
            }
        }


        /// <summary>
        /// صفحه ویرایش سیمکارت
        /// POST
        /// </summary>
        /// <returns></returns>
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
                return RedirectToAction("Index", "Error", new { Msg = ex.Message });
            }
        }

        /// <summary>
        /// صفحه حذف سیمکارت
        /// GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SimDTO sim = _simService.GetSimById(id.Value);
                if (sim == null)
                {
                    return HttpNotFound();
                }
                return View(sim);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Msg = ex.Message });
            }
        }

        /// <summary>
        /// صفحه حذف سیمکارت
        /// POST
        /// </summary>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _simService.DeleteSimById(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Msg = ex.Message });
            }
        }

        /// <summary>
        /// صفحه نمایش سیمکارت های حذف شده
        /// </summary>
        /// <returns></returns>
        public ActionResult DeletedSims()
        {
            return View(_simService.GetDeletedSims());
        }

        /// <summary>
        /// صفحه بازگردانی سیمکارت حذف شده
        /// </summary>
        /// <returns></returns>
        public ActionResult UnDelete(int id)
        {
            try
            {
                _simService.UnDeleteSimById(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Msg = ex.Message });
            }
        }


        /// <summary>
        /// صفحه شارژ/پرداخت قبض سیمکارت
        /// GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Wallet(int? id)
        {
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }

                var currentsim = _simService.GetWallet(id.Value);
                ViewBag.wallet = currentsim;
                var vm = new WalletChargeDTO
                {
                    SimId = id.Value,
                    AddBalance = 0,
                    SimTypeId = currentsim.SimTypeId
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { Msg = ex.Message });
            }
        }

        /// <summary>
        /// صفحه شارژ/پرداخت قبض سیمکارت
        /// POST
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Wallet([Bind] WalletChargeDTO walletChargeDTO)
        {

            try
            {
                switch (walletChargeDTO.SimTypeId)
                {
                    case (int)OperatorManagementBL.Enum.SimTypeEnum.credit:
                        {

                            if (walletChargeDTO.AddBalance <= 0)
                            {
                                ModelState.AddModelError("InvalidBalance", "مبلغ شارژ معتبر نیست");
                                ViewBag.wallet = _simService.GetWallet(walletChargeDTO.SimId);
                                return View(walletChargeDTO);
                            }

                            _simService.ChargeSim(walletChargeDTO.SimId, walletChargeDTO.AddBalance);
                            ViewBag.ChargeSuccess = true;

                            break;
                        }
                    case (int)OperatorManagementBL.Enum.SimTypeEnum.permanent:
                        {
                            _simService.PayBillSim(walletChargeDTO.SimId);
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
                return RedirectToAction("Index", "Error", new { Msg = ex.Message });
            }
        }
    }
}
