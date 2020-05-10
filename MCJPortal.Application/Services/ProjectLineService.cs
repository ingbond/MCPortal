using AutoMapper;
using AutoMapper.QueryableExtensions;
using MCJPortal.Application.Extensions;
using MCJPortal.Application.Models;
using MCJPortal.Application.Services.Interfaces;
using MCJPortal.Domain.Context;
using MCJPortal.Domain.Models;
using MCJPortal.ViewModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MCJPortal.Application.Services
{
    public class ProjectLineService : IProjectLineService
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRolePermissionsService _rolePermissionsService;

        public ProjectLineService(MainDbContext context, IMapper mapper, IRolePermissionsService rolePermissionsService)
        {
            _context = context;
            _mapper = mapper;
            _rolePermissionsService = rolePermissionsService;
        }

        public async Task<ProjectLineViewModel> Update(ProjectLineViewModel model, string userId)
        {
            var dbResult = await _context.ProjectLines
                .Include(x => x.Project)
                .Include(x => x.Document)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            _mapper.Map<ProjectLineViewModel, ProjectLine>(model, dbResult);

            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectLineViewModel>(dbResult);
        }

        public async Task<ProjectLineViewModel> GetProjectLineAsync(int id)
        {
            var result = await _context.ProjectLines
                .Include(x => x.Project)
                .Include(x => x.Document)
                .ProjectTo<ProjectLineViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<FilterResponseModel<ProjectLineTableViewModel>> GetProjectsFilterdAsync(FilterProjectLinesModel filter, string userId)
        {
            var result = new FilterResponseModel<ProjectLineTableViewModel>();
            var userRole = _context.Users
                .Include(x => x.UserRoles)
                .First(x => x.Id == userId)
                .UserRoles
                .FirstOrDefault();

            var query = _context.ProjectLines
                .Include(x => x.Project)
                .Include(x => x.Status)
                .ProjectTo<ProjectLineTableViewModel>(_mapper.ConfigurationProvider)
                .AsQueryable();

            query = FilterProjects(query, filter);

            var allRecordsCount = await query.CountAsync();

            // for pagination
            query = query.FilterByParams(filter);

            var resultData = await query.ToListAsync();

            if (userRole != null)
            {
                resultData = await SetToNullNotAllowedPropertiesAsync(resultData, userRole.RoleId);
            }

            result.Data = resultData;
            result.Total = allRecordsCount;

            return result;
        }

        private IQueryable<ProjectLineTableViewModel> FilterProjects(IQueryable<ProjectLineTableViewModel> query, FilterProjectLinesModel filter)
        {
            if (!string.IsNullOrEmpty(filter.ProjectName))
            {
                var name = filter.ProjectName.ToLower();
                query = query
                    .Where(x => x.ProjectName.ToLower().Contains(name));
            };

            if (filter.LineNumber != null)
            {
                query = query
                    .Where(x => x.Number == filter.LineNumber);
            };

            if (!string.IsNullOrEmpty(filter.Barcode))
            {
                var name = filter.Barcode.ToLower();
                query = query
                    .Where(x => x.Barcode.ToLower().Contains(name));
            };

            if (!string.IsNullOrEmpty(filter.Nickname))
            {
                var name = filter.Nickname.ToLower();
                query = query
                    .Where(x => x.NickNameAustralia.ToLower().Contains(name) ||
                                  x.NickNameIndia.ToLower().Contains(name));
            };

            if (filter.StatusId != null && filter.StatusId != Guid.Empty)
            {
                query = query
                    .Where(x => x.StatusId == filter.StatusId);
            };

            return query;
        }

        private async Task<List<ProjectLineTableViewModel>> SetToNullNotAllowedPropertiesAsync(List<ProjectLineTableViewModel> data, string userRoleId)
        {
            var notAllowedProperies = await _rolePermissionsService.GetNotAllowedPropertiesAsync(userRoleId);

            if (!notAllowedProperies.Any())
                return data;

            foreach (var record in data)
            {
                foreach (var prop in notAllowedProperies)
                {
                    PropertyInfo propInfo = record.GetType().GetProperty(prop, BindingFlags.Public | BindingFlags.Instance);

                    if (null != propInfo && propInfo.CanWrite)
                    {
                        propInfo.SetValue(record, null, null);
                    }
                }
            }

            return data;
        }
    }
}
