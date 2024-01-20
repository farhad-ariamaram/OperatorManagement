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

        // صفحه لیست تعرفه ها
        public ActionResult Index()
        {
            var vm = _costService.GetCosts();
            return View(vm);
        }

        // صفحه تغییر تعرفه
        // GET
        public ActionResult Set(int? Id)
        {
            if(Id == null)
            {
                return HttpNotFound();
            }

            CostDTO vm = _costService.GetCostById(Id.Value);

            return View(vm);
        }

        //صفحه تغییر تعرفه
        //POST
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
                return RedirectToAction("Index", "Error", new ErrorDTO { Msg = ex.Message, StatusCode = 500 });
            }
        }
    }
}