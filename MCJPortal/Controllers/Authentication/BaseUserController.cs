using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCJPortal.Controllers.Authentication
{
    public class BaseUserController : Controller
    {
        protected string GetUserId()
        {
            return User.GetClaim(OpenIdConnectConstants.Claims.Subject);
        }
    }
}
