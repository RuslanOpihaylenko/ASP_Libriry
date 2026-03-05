using Books.Application.DTOs.CountryDTOs;
using Books.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Queries.GetAllCountries
{
    public record GetAllCountriesQuery() : IRequest<ICollection<CountryReadDto>>;
}
