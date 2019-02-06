using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJ.BLL.Extensions
{
    public static class ModelStateExtension
    {
        public static string FirstErrorOrDefault(this ModelStateDictionary modelStateDictionary)
        {
            var result = modelStateDictionary.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).FirstOrDefault();
            return result;
        }
    }
}
