using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Training.Exceptions
{
    public class ExceptionsFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is UserFriendlyException userFriendlyException)
            {
                context.Result = new ObjectResult(new { Message = userFriendlyException.Message }) { StatusCode = 400 };
            }
            else if (context.Exception is Exception ex)
            {
                context.Result = new ObjectResult(new { Message = ex.Message }) { StatusCode = 500 };
            }

            context.ExceptionHandled = true;
        }
    }
}
