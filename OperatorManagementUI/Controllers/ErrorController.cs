using OperatorManagementBL.DTOs;
using System.Web.Mvc;

namespace OperatorManagementUI.Controllers
{
    public class ErrorController : Controller
    {
        // صفحه نمایش خطا به کاربر
        [HandleError]
        public ActionResult Index(ErrorDTO errorDto)
        {
            return View(errorDto);
        }
    }
}