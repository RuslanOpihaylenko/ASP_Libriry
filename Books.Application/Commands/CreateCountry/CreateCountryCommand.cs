using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;


namespace Books.Application.Commands.CreateCountry
{
    public record CreateCountryCommand(string name) : IRequest<int?>;
}
