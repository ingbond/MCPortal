using System.Collections.Generic;
using System.Threading.Tasks;

namespace MCJPortal.Application.Services
{
    public interface IRolePermissionsService
    {
        Task<List<string>> GetNotAllowedPropertiesAsync(string userRoleId);
        Task<List<string>> GetNotAllowedPropertiesForUserIdAsync(string userId);
    }
}