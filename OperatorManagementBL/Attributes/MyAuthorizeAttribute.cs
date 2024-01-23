using OperatorManagementBL.Services;
using System.Web.Mvc;

namespace OperatorManagementBL.Attributes
{
    public class MyAuthorizeAttribute : ActionFilterAttribute
    {
        AuthorizeService authorizeService = new AuthorizeService();

        public string Roles { get; set; } 

        public override async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.Session["UserID"];
            if (user == null)
            {
                filterContext.Result = new RedirectResult("/User/Login?redirectUrl=true");
                return;
            }

            string[] roles = Roles.Split(',');

            var isAuthorize = authorizeService.IsAuthorize(roles, (int)user);

            if (!isAuthorize)
                filterContext.Result = new RedirectResult("/Error/Index?Msg=اجازه دسترسی به صفحه را ندارید&StatusCode=500");
        }
    }
}
