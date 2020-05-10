using MCJPortal.Application.Services;
using MCJPortal.ViewModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCJPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<List<CountryViewModel>> GetCountries()
        {
            return await _countryService.GetCountries();
        }
    }
}
