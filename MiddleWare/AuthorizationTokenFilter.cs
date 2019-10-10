using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace PGS.WebApi.MiddleWare
{
    /// <summary>
    /// Filter
    /// </summary>
    public class AuthorizationTokenFilter : IAuthorizationFilter
    {
        
        /// <summary>
        /// Authorization
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string authHeader = context.HttpContext.Request.Headers["Authorization"];
        }
    }
}
