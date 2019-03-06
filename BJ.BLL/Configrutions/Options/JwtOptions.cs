using System;
using System.Collections.Generic;
using System.Text;

namespace BJ.BLL.Configrutions.Options
{
    public class JwtOptions
    {
        public string JwtKey {get;set;}
        public string JwtExpireDays { get; set; }
        public string JwtIssuer { get; set; }
    }
}
