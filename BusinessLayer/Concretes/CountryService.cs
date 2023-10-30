using AutoMapper;
using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Countries;
using Core.Utilities.Results;
using DataAccessLayer.Abstracts;
using EntityLayer.Concretes;

namespace BusinessLayer.Concretes
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository countryRepository;
        private readonly IMapper mapper;

        public CountryService(ICountryRepository countryRepository, IMapper mapper)
        {
            this.countryRepository = countryRepository;
            this.mapper = mapper;
        }

        public async Task<Result> AddCountry(AddCountryDto country)
        {
            var countryEntity = mapper.Map<Country>(country);
            await countryRepository.AddAsync(countryEntity);
            return new SuccessResult("Country added");
        }

        public async Task<Result> DeleteCountry(CountryDto country)
        {
            var countryEntity = mapper.Map<Country>(country);
            await countryRepository.RemoveAsync(countryEntity);
            return new SuccessResult("Country deleted");
        }

        public DataResult<IQueryable<Country>> GetAllCountriesAsQueryable()
        {
            var countryList = countryRepository.GetAll();
            return new SuccessDataResult<IQueryable<Country>>(countryList);
        }

        public DataResult<List<CountryDto>> GetAllCountryList()
        {
            var countryList = countryRepository.GetAll().ToList();
            var countryListDto = mapper.Map<List<CountryDto>>(countryList);
            return new SuccessDataResult<List<CountryDto>>("All countries listed", countryListDto);
        }

        public async Task<DataResult<CountryDto>> GetCountryById(int countryId)
        {
            var country = await countryRepository.GetByIdAsync(countryId);
            var countryDto = mapper.Map<CountryDto>(country);
            return new SuccessDataResult<CountryDto>("Country information listed", countryDto);
        }

        public async Task<DataResult<CountryDto>> UpdateCountry(AddCountryDto country, int countryId)
        {
            var countryEntity = await countryRepository.GetByIdAsync(countryId);
            if (countryEntity != null)
            {
                countryEntity.Code = country.Code ?? countryEntity.Code;
                countryEntity.Name = country.Name ?? countryEntity.Name;
                await countryRepository.Update(countryEntity);
                var mappedCountry = mapper.Map<CountryDto>(countryEntity);
                return new SuccessDataResult<CountryDto>("Country updated", mappedCountry);
            }
            return new ErrorDataResult<CountryDto>("Country couldn't update", null);
        }
    }
}
