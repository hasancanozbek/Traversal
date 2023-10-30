using BusinessLayer.Dtos.Cities;
using Core.Utilities.Results;
using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface ICityService
    {
        DataResult<List<CityDto>> GetAllCityList();
        DataResult<IQueryable<City>> GetAllCitiesAsQueryable();
        Task<DataResult<CityDto>> GetCityByCode(int code);
        Task<Result> AddCity(AddCityDto city);
        Task<DataResult<CityDto>> UpdateCity(AddCityDto city, int cityId);
        Task<Result> DeleteCity(CityDto city);
    }
}
