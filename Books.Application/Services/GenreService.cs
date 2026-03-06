using AutoMapper;
using Books.Application.DTOs.AuthorDTOs;
using Books.Application.DTOs.BookDTOs;
using Books.Application.DTOs.GenreDTOs;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;

namespace Books.Application.Services
{
    public class GenreService:IGenreService
    {
        private readonly IGenreRepository _repository;

        private readonly IMapper _mapper;
        private readonly ICachingService _cacheService;
        public GenreService(IGenreRepository repository, IMapper mapper, ICachingService cacheService)

        {

            _repository = repository;

            _mapper = mapper;
            _cacheService = cacheService;
        }
        //Create genre
        public async Task<int?> CreateGenreAsync(GenreCreateDto dto)

        {
            await _cacheService.RemoveAsync("Genres");
            var genre = _mapper.Map<GenreEntity>(dto);

            return await _repository.AddGenreAsync(genre);//, dto.Books);
        }
        //Get genre by id
        public async Task<GenreReadDto?> GetGenreByIdAsync(int id)

        {

            var genre = await _repository.GetGenreByIdAsync(id);

            if (genre == null) return null;

            var dto = _mapper.Map<GenreReadDto>(genre);

            return dto;

        }
        //Get all genres
        public async Task<ICollection<GenreReadDto>> GetAllGenresAsync()

        {
            var cache = await _cacheService.GetAsync<ICollection<GenreReadDto>>("Genres");
            if (cache == null)
            {
                var genres = await _repository.GetAllGenreAsync();
                cache = _mapper.Map<ICollection<GenreReadDto>>(genres);
                await _cacheService.SetAsync("Genres", cache, null);
            }
            return cache;
        }
        //Search genres
        public async Task<ICollection<GenreReadDto>> SearchGenresAsync(string title)
        {
            var genres = await _repository.SearchGenresAsync(title);
            return _mapper.Map<ICollection<GenreReadDto>>(genres);
        }
        //Update genre
        public async Task<GenreReadDto?> UpdeteGenreAsync(int id, GenreUpdateDto dto)
        {
            await _cacheService.RemoveAsync("Genres");
            var entity = _mapper.Map<GenreEntity>(dto);
            var update = await _repository.UpdeteGenreById(id, entity);

            if (update == null) return null;

            return _mapper.Map<GenreReadDto>(update);
        }
        //Delete by id
        public async Task<bool> DeleteGenreAsync(int id)
        {
            await _cacheService.RemoveAsync("Genres");
            var genre = await _repository.GetGenreByIdAsync(id);

            if (genre == null)
                return false;

            var result = await _repository.DeleteGenreAsync(genre);

            return result != null && result > 0;
        }
        //Delete all genre
        public async Task<int> DeleteAllGenresAsync()
        {
            await _cacheService.RemoveAsync("Genres");
            var deleted = await _repository.DeleteAllGenresAsync();
            return deleted.Count;
        }
    }
}
