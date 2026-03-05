using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Repositories
{
    public interface ICityRepository
    {
        Task<ICollection<CityEntity?>> GetAllCitiesAsync();
        Task<int?> AddCityAsync(CityEntity city);
        Task<CityEntity> UpdeteCityById(int id, CityEntity updateCity);
        Task<int?> DeleteCityAsync(CityEntity city);
    }
}
