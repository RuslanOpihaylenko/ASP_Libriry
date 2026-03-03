using AutoMapper;
using Books.Application.DTOs.AuthorDTOs;
using Books.Application.DTOs.GenreDTOs;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;

namespace Books.Application.Services
{
    public class AuthorService:IAuthorService
    {
        private readonly IAuthorRepository _repository;

        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository repository, IMapper mapper)

        {

            _repository = repository;

            _mapper = mapper;

        }
        //Create author
        public async Task<int?> CreateAuthorAsync(AuthorCreateDto dto)

        {
            var author = _mapper.Map<AuthorEntity>(dto);

            return await _repository.AddAuthorAsync(author);//, dto.Books);
        }
        //Get author by id
        public async Task<AuthorReadDto?> GetAuthorByIdAsync(int id)

        {

            var author = await _repository.GetAuthorByIdAsync(id);

            if (author == null) return null;

            var dto = _mapper.Map<AuthorReadDto>(author);

            return dto;

        }
        //Get all authors
        public async Task<ICollection<AuthorReadDto>> GetAllAuthorsAsync()

        {
            var authors = await _repository.GetAllAuthorAsync();

            return _mapper.Map<ICollection<AuthorReadDto>>(authors);

        }
        //Search authors
        public async Task<ICollection<AuthorReadDto>> SearchAuthorsAsync(string name)
        {
            var authors = await _repository.SearchAuthorsAsync(name);
            return _mapper.Map<ICollection<AuthorReadDto>>(authors);
        }
        //Update author
        public async Task<AuthorReadDto?> UpdeteAuthorAsync(int id, AuthorUpdateDto dto)
        {
            var entity = _mapper.Map<AuthorEntity>(dto);
            var update = await _repository.UpdeteAuthorById(id, entity);

            if (update == null) return null;

            return _mapper.Map<AuthorReadDto>(update);
        }
        //Delete by id
        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _repository.GetAuthorByIdAsync(id);

            if (author == null)
                return false;

            var result = await _repository.DeleteAuthorAsync(author);

            return result != null && result > 0;
        }
        //Delete all author
        public async Task<int> DeleteAllAuthorsAsync()
        {
            var deleted = await _repository.DeleteAllAuthorsAsync();
            return deleted.Count;
        }
    }
}
