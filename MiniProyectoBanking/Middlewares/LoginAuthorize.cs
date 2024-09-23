using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using MiniProyectoBanking.Controllers;
using MiniProyectoBanking.Middlewares;

namespace MiniProyectoBanking.Middlewares
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        private readonly ValidateUserSession _userSession;

        public LoginAuthorize(ValidateUserSession userSession)
        {
            _userSession = userSession;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,ActionExecutionDelegate next)
        {
            if (_userSession.HasUser())
            {
                var controller = (LoginController)context.Controller;
                context.Result = controller.RedirectToAction("index", "home");
            }
            else
            {
                await next();
            }
        }
    }
}
