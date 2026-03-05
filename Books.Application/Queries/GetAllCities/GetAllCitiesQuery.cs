using Books.Application.DTOs.CityDTOs;
using Books.Application.DTOs.CountryDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Queries.GetAllCities
{
    public record GetAllCitiesQuery() : IRequest<ICollection<CityReadDto>>;
}
