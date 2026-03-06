using AutoMapper;
using Books.Application.DTOs.AuthorDTOs;
using Books.Application.DTOs.GenreDTOs;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;
using System.Net.Http.Headers;

namespace Books.Application.Services
{
    public class AuthorService:IAuthorService
    {
        private readonly IAuthorRepository _repository;

        private readonly IMapper _mapper;
        private readonly ICachingService _cacheService;

        public AuthorService(IAuthorRepository repository, IMapper mapper, ICachingService cacheService)

        {

            _repository = repository;

            _mapper = mapper;
            _cacheService = cacheService;
        }
        //Create author
        public async Task<int?> CreateAuthorAsync(AuthorCreateDto dto)
        {
            await _cacheService.RemoveAsync("Authors");
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
            var cache = await _cacheService.GetAsync<ICollection<AuthorReadDto>>("Authors");
            if (cache == null)
            {
                var authors = await _repository.GetAllAuthorAsync();
                cache = _mapper.Map<ICollection<AuthorReadDto>>(authors);
                await _cacheService.SetAsync("Authors", cache, null);
            }
            return cache;
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
            await _cacheService.RemoveAsync("Authors");
            var entity = _mapper.Map<AuthorEntity>(dto);
            var update = await _repository.UpdeteAuthorById(id, entity);

            if (update == null) return null;

            return _mapper.Map<AuthorReadDto>(update);
        }
        //Delete by id
        public async Task<bool> DeleteAuthorAsync(int id)
        {
            await _cacheService.RemoveAsync("Authors");
            var author = await _repository.GetAuthorByIdAsync(id);

            if (author == null)
                return false;

            var result = await _repository.DeleteAuthorAsync(author);

            return result != null && result > 0;
        }
        //Delete all author
        public async Task<int> DeleteAllAuthorsAsync()
        {
            await _cacheService.RemoveAsync("Authors");
            var deleted = await _repository.DeleteAllAuthorsAsync();
            return deleted.Count;
        }
    }
}
