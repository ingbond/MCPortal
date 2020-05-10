using MCJPortal.Application.Models;
using MCJPortal.ViewModels.ViewModels;
using System.Threading.Tasks;

namespace MCJPortal.Application.Services
{
    public interface IProjectLineService
    {
        Task<ProjectLineViewModel> GetProjectLineAsync(int id);
        Task<FilterResponseModel<ProjectLineTableViewModel>> GetProjectsFilterdAsync(FilterProjectLinesModel filter, string userId);
        Task<ProjectLineViewModel> Update(ProjectLineViewModel model, string userId);
    }
}