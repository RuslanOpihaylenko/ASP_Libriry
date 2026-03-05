using Books.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Commands.UpdateCity
{
    public record UpdateCityCommand(int Id, string Name) : IRequest<CityEntity?>;
}
