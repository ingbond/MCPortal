using System.Threading.Tasks;
using AspNet.Security.OAuth.Validation;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using MCJPortal.Application.Models;
using MCJPortal.Application.Services;
using MCJPortal.Application.Services.Interfaces;
using MCJPortal.ViewModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCJPortal.Controllers
{
    [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectLineController : ControllerBase
    {
        private readonly IProjectLineService _projectLineService;

        public ProjectLineController(IProjectLineService projectLineService)
        {
            this._projectLineService = projectLineService;
        }

        [HttpPost("[action]")]
        public async Task<ProjectLineViewModel> Update(ProjectLineViewModel model)
        {
            var userId = User.GetClaim(OpenIdConnectConstants.Claims.Subject);
            return await _projectLineService.Update(model, userId);
        }

        [HttpPost("[action]")]
        public async Task<FilterResponseModel<ProjectLineTableViewModel>> GetProjectLines(FilterProjectLinesModel model)
        {
            var userId = User.GetClaim(OpenIdConnectConstants.Claims.Subject);
            return await _projectLineService.GetProjectsFilterdAsync(model, userId);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ProjectLineViewModel> GetProjectLine(int id)
        {
            return await _projectLineService.GetProjectLineAsync(id);
        }
    }
}