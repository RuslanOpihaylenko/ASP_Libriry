using Books.Application.Interfaces.Repositories;
using Books.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Commands.UpdateCity
{
    public class UpdateCityHandler : IRequestHandler<UpdateCityCommand, CityEntity?>
    {
        private readonly ICityRepository _repository;

        public UpdateCityHandler(ICityRepository repository)
        {
            _repository = repository;
        }

        public async Task<CityEntity?> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var updatedCity = new CityEntity
            {
                Name = request.Name
            };

            var result = await _repository.UpdeteCityById(request.Id, updatedCity);

            return result;
        }
    }
}
