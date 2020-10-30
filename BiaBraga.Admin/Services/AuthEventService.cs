using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Threading.Tasks;

namespace BiaBraga.Admin.Services
{
    public class AuthEventService : CookieAuthenticationEvents
    {

        private IUrlHelperFactory _helper;
        private IActionContextAccessor _accessor;
        public AuthEventService(IUrlHelperFactory helper, IActionContextAccessor accessor)
        {
            _helper = helper;
            _accessor = accessor;
        }

        public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
            context.RedirectUri = "/Users/Login";

            return base.RedirectToLogin(context);
        }

        public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
        {
            context.RedirectUri = "/Home/Restrito";

            return base.RedirectToAccessDenied(context);
        }
    }
}
