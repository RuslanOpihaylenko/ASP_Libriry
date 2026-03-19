using AutoMapper;
using Books.Application.DTOs.AuthorDTOs;
using Books.Application.DTOs.GenreDTOs;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Application.Mapping;
using Books.Application.Services;
using Books.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Tests
{
    public class GenreServiceTest
    {
        private readonly GenreService _service;
        private readonly Mock<IGenreRepository> _genreRepoMock;
        private readonly Mock<ICachingService> _cacheMock;
        private readonly IMapper _mapper;
        private readonly ILoggerFactory _loggerFactory;


        public GenreServiceTest()
        {
            _genreRepoMock = new Mock<IGenreRepository>();
            _cacheMock = new Mock<ICachingService>();

            _loggerFactory = LoggerFactory.Create(builder => { });

            // Налаштовуємо AutoMapper для простого тесту
            // Створюємо конфігурацію і додаємо твій профіль

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GenreProfile>();
            }, _loggerFactory);

            // (опціонально) перевіряємо, що всі мапінги валідні
            config.AssertConfigurationIsValid();
            _mapper = new Mapper(config);
            _service = new GenreService(_genreRepoMock.Object, _mapper, _cacheMock.Object);
        }

        [Fact]
        public async Task GetAllGenresAsync_ShouldReturnAuthors_FromCache_WhenCacheExists()
        {
            // Arrange: кеш вже містить авторів
            var cachedAuthors = new List<GenreReadDto>
        {
            new GenreReadDto { Id = 1, Title = "Genre1" }
        };

            _cacheMock.Setup(c => c.GetAsync<ICollection<GenreReadDto>>("Genres"))
                      .ReturnsAsync(cachedAuthors);

            // Act
            var result = await _service.GetAllGenresAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result); //колекція містить лише 1 елемент
            Assert.Equal("Genre1", result.First().Title);

            // Репозиторій не має викликатися, бо кеш є
            _genreRepoMock.Verify(r => r.GetAllGenreAsync(), Times.Never);
        }


        [Fact]
        public async Task GetAllGenresAsync_CacheEmpty_FetchesFromRepositoryAndSetsCache()
        {
            // Arrange
            var genresFromRepo = new List<GenreEntity>
        {
            new GenreEntity { Id = 1, Title = "Genre1" },
            new GenreEntity { Id = 2, Title = "Genre2" }
        };

            // Кеш порожній
            _cacheMock.Setup(c => c.GetAsync<ICollection<GenreReadDto>>("Genres"))
                      .ReturnsAsync((ICollection<GenreReadDto>)null);

            // Репозиторій повертає дані
            _genreRepoMock.Setup(r => r.GetAllGenreAsync())
                           .ReturnsAsync(genresFromRepo);

            var service = new GenreService(_genreRepoMock.Object, _mapper, _cacheMock.Object);

            // Act
            var result = await service.GetAllGenresAsync();

            // Assert
            Assert.Equal(2, result.Count); // перевіряємо, що повернуло два елементи
            Assert.Contains(result, a => a.Title == "Genre1");
            Assert.Contains(result, a => a.Title == "Genre2");

            // Перевіряємо, що кеш було встановлено (TimesOnce перевірка, щоб метод був викликаний 1 раз)
            _cacheMock.Verify(c => c.SetAsync("Genres", It.IsAny<ICollection<GenreReadDto>>(), null), Times.Once);
        }
        [Fact]
        public async Task GetGenreByIdAsync_GenreExists_ReturnsMappedDto()
        {
            // Arrange
            var genreId = 1;

            var genreFromRepo = new GenreEntity
            {
                Id = genreId,
                Title = "TestGenre"
            };

            // Репозиторий возвращает сущность
            _genreRepoMock.Setup(r => r.GetGenreByIdAsync(genreId))
                          .ReturnsAsync(genreFromRepo);

            var service = new GenreService(_genreRepoMock.Object, _mapper, _cacheMock.Object);

            // Act
            var result = await service.GetGenreByIdAsync(genreId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(genreId, result.Id);
            Assert.Equal("TestGenre", result.Title);
        }

        [Fact]
        public async Task GetGenreByIdAsync_GenreDoesNotExist_ReturnsNull()
        {
            // Arrange
            var genreId = 1;

            // Репозиторий возвращает null
            _genreRepoMock.Setup(r => r.GetGenreByIdAsync(genreId))
                          .ReturnsAsync((GenreEntity)null);

            var service = new GenreService(_genreRepoMock.Object, _mapper, _cacheMock.Object);

            // Act
            var result = await service.GetGenreByIdAsync(genreId);

            // Assert
            Assert.Null(result);
        }
    }
}
