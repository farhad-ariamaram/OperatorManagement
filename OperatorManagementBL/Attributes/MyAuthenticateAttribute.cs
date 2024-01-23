using System.Web.Mvc;

namespace OperatorManagementBL.Attributes
{
    public class MyAuthenticateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.Session["UserID"];
            if (user == null)
                filterContext.Result = new RedirectResult("/User/Login?redirectUrl=true");
        }
    }
}
