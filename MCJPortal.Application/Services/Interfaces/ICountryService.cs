using MCJPortal.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MCJPortal.Application.Services
{
    public interface ICountryService
    {
        Task<List<CountryViewModel>> GetCountries();
    }
}