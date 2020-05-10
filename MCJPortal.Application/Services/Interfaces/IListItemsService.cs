using MCJPortal.Domain.Models.Enums;
using MCJPortal.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MCJPortal.Application.Services.Interfaces
{
    public interface IListItemsService
    {
        Task<List<ListItemViewModel>> GetItemsForTypeAsync(ListTypeEnum type);
    }
}