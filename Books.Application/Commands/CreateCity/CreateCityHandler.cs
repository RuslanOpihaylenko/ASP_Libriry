using Books.Application.Commands.CreateCountry;
using Books.Application.Interfaces.Repositories;
using Books.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Commands.CreateCity
{
    public class CreateCityHandler : IRequestHandler<CreateCityCommand, int?>
    {
        private readonly ICityRepository _repository;
        public CreateCityHandler(ICityRepository repository)
        {
            _repository = repository;
        }
        public async Task<int?> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var city = new CityEntity();
            city.Name = request.name;
            var result = await _repository.AddCityAsync(city);
            return result;
        }
    }
}
