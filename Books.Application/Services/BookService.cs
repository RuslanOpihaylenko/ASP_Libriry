using AutoMapper;

using Books.Application.DTOs.BookDTOs;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;


namespace Books.Application.Services;

public class BookService : IBookService

{

    private readonly IBookRepository _repository;

    private readonly IMapper _mapper;

    public BookService(IBookRepository repository, IMapper mapper)

    {

        _repository = repository;

        _mapper = mapper;

    }

    // Створення книги

    public async Task<int?> CreateBookAsync(BookCreateDto dto)

    {

        var book = _mapper.Map<BookEntity>(dto);

        return await _repository.AddBookAsync(book, dto.AuthorsId);

    }

    // Отримати книгу по Id

    public async Task<BookReadDto?> GetBookByIdAsync(int id)

    {

        var book = await _repository.GetBookByIdAsync(id);

        if (book == null) return null;

        var dto = _mapper.Map<BookReadDto>(book);

        return dto;

    }

    // Отримати всі книги

    public async Task<ICollection<BookReadDto>> GetAllBooksAsync()

    {
        var books = await _repository.GetAllBooksAsync();

        return _mapper.Map<ICollection<BookReadDto>>(books);

    }
    //Get chunk
    public async Task<ICollection<BookReadDto>> GetChunkAsync(int pagenum, int limit)
    {
        var books = await _repository.GetChunk(pagenum, limit);
        return _mapper.Map<ICollection<BookReadDto>>(books);
    }
    //Search books
    public async Task<ICollection<BookReadDto>> SearchBooksAsync(
        string? author, int? year, string? genre)
    {
        var books = await _repository.SearchBooksAsync(author, year, genre);
        return _mapper.Map<ICollection<BookReadDto>>(books);
    }
    //Update book
    public async Task<BookReadDto?> UpdeteBookAsync(int id, BookUpdateDto dto)
    {
        var entity = _mapper.Map<BookEntity>(dto);
        var update = await _repository.UpdeteBookById(id, entity);

        if (update == null) return null;

        return _mapper.Map<BookReadDto>(update);
    }
    //Delete by id
    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _repository.GetBookByIdAsync(id);

        if (book == null)
            return false;

        var result = await _repository.DeleteBookAsync(book);

        return result != null && result > 0;
    }
    //Delete all book
    public async Task<int> DeleteAllBooksAsync()
    {
        var deleted = await _repository.DeleteAllBooksAsync();
        return deleted.Count;
    }
}

