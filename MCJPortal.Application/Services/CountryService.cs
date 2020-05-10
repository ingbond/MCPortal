using AutoMapper;
using AutoMapper.QueryableExtensions;
using MCJPortal.Domain.Context;
using MCJPortal.ViewModels.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MCJPortal.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public CountryService(MainDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<List<CountryViewModel>> GetCountries()
        {
            var dbResult = await _context.Countries
                .ProjectTo<CountryViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return dbResult;
        }
    }
}
