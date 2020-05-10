using MCJPortal.Application.Models;
using MCJPortal.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MCJPortal.Application.Services.Interfaces
{
    public interface IProjectsService
    {
        Task<List<ProjectViewModel>> GetUserProjectsAsync(string userId);
        Task<List<ProjectViewModel>> GetProjectsAsync();
        Task<ProjectViewModel> GetProjectDetailsAsync(int projectId);
    }
}