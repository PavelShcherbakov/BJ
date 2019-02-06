using BJ.BLL.Commons;
using BJ.BLL.Exceptions;
using BJ.BLL.Extensions;
using BJ.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BJ.WEB.Controllers
{
    public class BaseController:Controller
    {
        protected async Task<IActionResult> Execute<TempType>(Func<Task<TempType>> func)
        {
            GenericResponseView<TempType> response = new GenericResponseView<TempType>();
            try
            {
                if (!ModelState.IsValid)
                {
                    var errorResult = new GenericResponseView<string>();
                    errorResult.Error = ModelState.FirstErrorOrDefault();
                    return BadRequest(errorResult);
                }

                var result = await func();
                response.Model = result;
                return Ok(response);
            }
            catch (CustomServiceException ex)
            {
                response.Error = ex.Message;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                //await _loggerService.LogException(ex);
                response.Error = Constants.Messages.ServerError;
                return BadRequest(response);
            }
        }

        protected async Task<IActionResult> Execute(Func<Task> func)
        {
            var response = new GenericResponseView<string>();
            try
            {

                if (!ModelState.IsValid)
                {
                    var errorResult = new GenericResponseView<string>();
                    errorResult.Error = ModelState.FirstErrorOrDefault();
                    return BadRequest(errorResult);
                }

                await func();
                return Ok(response);
            }
            catch (CustomServiceException ex)
            {
                response.Error = ex.Message;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                //await _loggerService.LogException(ex);
                response.Error = Constants.Messages.ServerError;
                return BadRequest(response);
            }
        }
    }
}
