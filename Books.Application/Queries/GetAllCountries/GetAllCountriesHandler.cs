using AutoMapper;
using Books.Application.DTOs.BookDTOs;
using Books.Application.DTOs.CountryDTOs;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Application.Queries.GetAllCountries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Queries.GetAllCountries
{
    public class GetAllCountriesHandler : IRequestHandler<GetAllCountriesQuery, ICollection<CountryReadDto>>
    {
        private readonly ICountryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICachingService _cacheService;
        public GetAllCountriesHandler(ICountryRepository repository, IMapper mapper, ICachingService cacheService)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<ICollection<CountryReadDto>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var cache = await _cacheService.GetAsync<ICollection<CountryReadDto>>("Countries");
            if (cache == null)
            {
                var entities = await _repository.GetAllContriesAsync();
                cache = _mapper.Map<ICollection<CountryReadDto>>(entities);
                await _cacheService.SetAsync("Countries", cache, null);
            }
            return cache;
        }
    }
}
