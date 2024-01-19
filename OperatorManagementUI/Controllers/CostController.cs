using OperatorManagementBL.DTOs;
using OperatorManagementBL.Services;
using System.Web.Mvc;

namespace OperatorManagementUI.Controllers
{
    public class CostController : Controller
    {
        private readonly ICostService _costService;

        public CostController()
        {
            _costService = new CostService();
        }

        public ActionResult Index()
        {
            var vm = _costService.GetCosts();
            return View(vm);
        }

        public ActionResult Set(int? Id)
        {
            if(Id == null)
            {
                return HttpNotFound();
            }

            CostDTO vm = _costService.GetCostById(Id.Value);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Set([Bind(Include = "Id,Value")] CostDTO cost)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(cost);
                }

                _costService.UpdateCost(cost);

                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                return HttpNotFound();
            }
        }
    }
}