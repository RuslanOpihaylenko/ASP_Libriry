using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Commands.CreateCity
{
    public record CreateCityCommand(string name) : IRequest<int?>;
}
