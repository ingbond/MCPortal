using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Validation;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using MCJPortal.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MCJPortal.Controllers
{
    [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IRolePermissionsService _rolePermissionsService;

        public PermissionsController(IRolePermissionsService rolePermissionsService)
        {
            _rolePermissionsService = rolePermissionsService;
        }

        [HttpGet("[action]")]
        public async Task<List<string>> GetNotAllowedProperties()
        {
            var userId = User.GetClaim(OpenIdConnectConstants.Claims.Subject);
            return await _rolePermissionsService.GetNotAllowedPropertiesForUserIdAsync(userId);
        }
    }
}