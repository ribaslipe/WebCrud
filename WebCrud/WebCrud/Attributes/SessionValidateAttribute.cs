using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Util;

namespace WebCrud.Attributes
{
    public class SessionValidateAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _memory = context.HttpContext.RequestServices.GetRequiredService<Memory>();
            Controller? controller = context.Controller as Controller;
            var token = _memory.GetMemoryItem<string>("Token");
            if (token == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Não Autorizado"
                };

                context.Result = new RedirectToActionResult("Error", "Home", null);
            }

            await next();
        }

    }
}
