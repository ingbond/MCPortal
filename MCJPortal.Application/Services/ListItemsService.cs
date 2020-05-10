using AutoMapper;
using MCJPortal.Application.Services.Interfaces;
using MCJPortal.Domain.Context;
using MCJPortal.Domain.Models.Enums;
using MCJPortal.ViewModels.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCJPortal.Application.Services
{
    public class ListItemsService : IListItemsService
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public ListItemsService(MainDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<List<ListItemViewModel>> GetItemsForTypeAsync(ListTypeEnum type)
        {
            var dbResult = await _context.ListItems
                .Include(x => x.ListType)
                .Include(x => x.Permissions)
                .Where(x => x.ListType.Id == type)
                .ToListAsync();
            var result = dbResult.Select(x => _mapper.Map<ListItemViewModel>(x)).ToList();

            return result;
        }
    }
}
