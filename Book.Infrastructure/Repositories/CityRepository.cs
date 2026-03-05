using Books.Application.Interfaces.Repositories;
using Books.Domain.Entities;
using Books.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly LibraryDBContext _context;
        public CityRepository(LibraryDBContext context)
        {
            _context = context;
        }

        public async Task<int?> AddCityAsync(CityEntity city)
        {
            await _context.Cities.AddAsync(city);
            await _context.SaveChangesAsync();
            return city.Id;
        }

        public async Task<ICollection<CityEntity?>> GetAllCitiesAsync()
        {
            return await _context.Cities.ToListAsync();
        }
        public async Task<int?> DeleteCityAsync(CityEntity city)
        {
            if (city == null)
            {
                return null;
            }
            _context.Cities.Remove(city);
            return await _context.SaveChangesAsync();
        }
        public async Task<CityEntity> UpdeteCityById(int id, CityEntity updateCity)
        {
            var isExist = await _context.Cities
                 .FirstOrDefaultAsync(b => b.Id == id);

            if (isExist == null)
            {
                return null;
            }

            isExist.Name = updateCity.Name;

            await _context.SaveChangesAsync();

            return isExist;
        }
    }
}
