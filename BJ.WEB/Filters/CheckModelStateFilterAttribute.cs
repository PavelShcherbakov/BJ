using BJ.BLL.Extensions;
using BJ.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace BJ.WEB.Filters
{
    public class CheckModelStateFilterAttribute : Attribute, IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorResult = new GenericResponseView<string>();
                errorResult.Error = context.ModelState.FirstErrorOrDefault();
                context.Result = new BadRequestObjectResult(errorResult);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
