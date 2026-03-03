using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Books.Application.DTOs.AuthorDTOs;
using Books.Application.DTOs.GenreDTOs;
using Books.Domain.Entities;

namespace Books.Application.Mapping
{
    public class GenreProfile:Profile
    {
        public GenreProfile() 
        {
            CreateMap<GenreCreateDto, GenreEntity>()
                .ForMember(dest => dest.Books, opt => opt.Ignore());

            CreateMap<GenreEntity, GenreReadDto>();
                //.ForMember(dest => dest.BooksId, opt => opt.MapFrom(src => src.Books.Select(a => a.Id)));
            CreateMap<GenreUpdateDto, GenreEntity>()
                .ForMember(dest => dest.Books, opt => opt.Ignore());
        }
    }
}
