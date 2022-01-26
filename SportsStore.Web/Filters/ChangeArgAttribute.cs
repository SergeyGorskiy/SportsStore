using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SportsStore.Web.Filters
{
    public class ChangeArgAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.ContainsKey("message1"))
            {
                context.ActionArguments["message1"] = "New message";
            }

            await next();
        }
    }
}
