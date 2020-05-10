using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Validation;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using MCJPortal.Application.Models;
using MCJPortal.Application.Services.Interfaces;
using MCJPortal.ViewModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MCJPortal.Controllers
{
    [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectsService;

        public ProjectsController(IProjectsService projectsService)
        {
            this._projectsService = projectsService;
        }

        [HttpGet("[action]/{id}")]
        public async Task<List<ProjectViewModel>> GetUserProjects(string userId)
        {
            return await _projectsService.GetUserProjectsAsync(userId);
        }

        [HttpGet]
        public async Task<List<ProjectViewModel>> GetProjects()
        {
            return await _projectsService.GetProjectsAsync();
        }

        

        [HttpGet("[action]/{projectId}")]
        public async Task<ProjectViewModel> GetProjectDetails(int projectId)
        {
            return await _projectsService.GetProjectDetailsAsync(projectId);
        }
    }
}