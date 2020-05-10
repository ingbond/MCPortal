using AutoMapper;
using AutoMapper.QueryableExtensions;
using MCJPortal.Application.Extensions;
using MCJPortal.Application.Models;
using MCJPortal.Application.Services.Interfaces;
using MCJPortal.Domain.Context;
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
    public class ProjectsService : IProjectsService
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRolePermissionsService _rolePermissionsService;

        public ProjectsService(MainDbContext context, IMapper mapper, IRolePermissionsService rolePermissionsService)
        {
            _context = context;
            _mapper = mapper;
            _rolePermissionsService = rolePermissionsService;
        }

        public async Task<List<ProjectViewModel>> GetUserProjectsAsync(string userId)
        {
            var dbResult = _context.Projects.Where(x => x.CreatedBy == userId || x.UpdatedBy == userId).AsQueryable();

            var result = await dbResult.Select(x => _mapper.Map<ProjectViewModel>(x)).ToListAsync();

            return result;
        }

        public async Task<List<ProjectViewModel>> GetProjectsAsync()
        {
            var dbResult = await _context.Projects.ToListAsync();

            var result = dbResult.Select(x => _mapper.Map<ProjectViewModel>(x)).ToList();

            return result;
        }

        public async Task<ProjectViewModel> GetProjectDetailsAsync(int projectId)
        {
            var dbProject = await _context.Projects.Include(x => x.ProjectLines).FirstOrDefaultAsync(x => x.Id == projectId);

            if (dbProject == null)
                throw new Exception("Project not found");

            return _mapper.Map<ProjectViewModel>(dbProject);
        }       
    }
}