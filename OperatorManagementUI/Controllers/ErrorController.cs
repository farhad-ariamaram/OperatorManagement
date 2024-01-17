using OperatorManagementBL.DTOs;
using System.Web.Mvc;

namespace OperatorManagementUI.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(ErrorDTO errorDto)
        {
            return View(errorDto);
        }
    }
}