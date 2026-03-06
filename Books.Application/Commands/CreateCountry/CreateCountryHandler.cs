using Books.Application.DTOs.BookDTOs;
using Books.Application.DTOs.CountryDTOs;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Commands.CreateCountry
{
    public class CreateCountryHandler : IRequestHandler<CreateCountryCommand, int?>
    {
        private readonly ICountryRepository _repository;
        private readonly ICachingService _cacheService;
        public CreateCountryHandler(ICountryRepository repository, ICachingService cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;
        }
        public async Task<int?> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            await _cacheService.RemoveAsync("Countries");
            var country = new CountryEntity();
            country.Name = request.name;
            var result = await _repository.AddCountryAsync(country);
            return result;
        }
    }
}
