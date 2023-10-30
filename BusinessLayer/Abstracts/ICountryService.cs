using BusinessLayer.Dtos.Countries;
using Core.Utilities.Results;
using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface ICountryService
    {
        DataResult<List<CountryDto>> GetAllCountryList();
        DataResult<IQueryable<Country>> GetAllCountriesAsQueryable();
        Task<DataResult<CountryDto>> GetCountryById(int countryId);
        Task<DataResult<CountryDto>> UpdateCountry(AddCountryDto country, int countryId);
        Task<Result> AddCountry(AddCountryDto country);
        Task<Result> DeleteCountry(CountryDto country);
    }
}
