using BJ.BLL.Commons;
using BJ.BLL.Exceptions;
using BJ.BLL.Extensions;
using BJ.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BJ.WEB.Controllers
{
    public class BaseController:Controller
    {

        public string UserId
        {
            get
            {
                return User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            }
        }

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
                response.Error = ex.Message;
                //response.Error = Constants.Messages.ServerError;
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
                //response.Error = ex.Message;
                response.Error = Constants.Messages.ServerError;
                return BadRequest(response);
            }
        }
    }
}
