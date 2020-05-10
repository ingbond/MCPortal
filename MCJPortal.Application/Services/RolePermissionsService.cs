using AutoMapper;
using MCJPortal.Domain.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCJPortal.Application.Services
{
    public class RolePermissionsService : IRolePermissionsService
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public RolePermissionsService(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<string>> GetNotAllowedPropertiesAsync(string userRoleId)
        {
            var notAllowedProperies = await _context.RolePermissions
                   .Include(x => x.Permission)
                   .Where(x => x.RoleId == userRoleId && x.Access == Domain.Models.Enums.AccessEnum.NoAccess)
                   .Select(x => x.Permission.Value)
                   .ToListAsync();

            return notAllowedProperies;
        }

        public async Task<List<string>> GetNotAllowedPropertiesForUserIdAsync(string userId)
        {
            var userRole = _context.Users
                .Include(x => x.UserRoles)
                .First(x => x.Id == userId)
                .UserRoles
                .FirstOrDefault();

            var notAllowedProperies = await GetNotAllowedPropertiesAsync(userRole.RoleId);

            return notAllowedProperies;
        }
    }
}
