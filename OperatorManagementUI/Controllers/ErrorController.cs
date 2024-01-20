using OperatorManagementBL.DTOs;
using System.Web.Mvc;

namespace OperatorManagementUI.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// صفحه نمایش خطا به کاربر
        /// </summary>
        /// <param name="errorDto">مدل صفحه که فقط شامل پیام خطا است</param>
        /// <returns></returns>
        [HandleError]
        public ActionResult Index(ErrorDTO errorDto)
        {
            return View(errorDto);
        }
    }
}