using AutoMapper;
using Books.Application.Commands.CreateCity;
using Books.Application.Commands.CreateCountry;
using Books.Application.DTOs.CityDTOs;
using Books.Application.Interfaces.Repositories;
using Books.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Queries.GetAllCities
{
    public class GetAllCitiesHandler : IRequestHandler<GetAllCitiesQuery, ICollection<CityReadDto>>
    {
        private readonly ICityRepository _repository;
        private readonly IMapper _mapper;
        public GetAllCitiesHandler(ICityRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ICollection<CityReadDto>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await _repository.GetAllCitiesAsync();
            return _mapper.Map<ICollection<CityReadDto>>(cities);

        }
    }
}
