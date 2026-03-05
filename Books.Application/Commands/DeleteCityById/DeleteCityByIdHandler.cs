using Books.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Commands.DeleteCityById
{
    public class DeleteCityByIdHandler : IRequestHandler<DeleteCityByIdCommand, int?>
    {
        private readonly ICityRepository _repository;

        public DeleteCityByIdHandler(ICityRepository repository)
        {
            _repository = repository;
        }

        public async Task<int?> Handle(DeleteCityByIdCommand request, CancellationToken cancellationToken)
        {
            var cities = await _repository.GetAllCitiesAsync();
            var city = cities.FirstOrDefault(c => c.Id == request.Id);
            var result = await _repository.DeleteCityAsync(city);
            return result;
        }
    }
}
