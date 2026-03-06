using AutoMapper;
using Books.Application.DTOs.BookDTOs;
using Books.Application.DTOs.CountryDTOs;
using Books.Application.DTOs.GenreDTOs;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Mapping
{
    public class CountryProfile:Profile
    {
        public CountryProfile()
        {
            CreateMap<CountryReadDto, CountryEntity>()
                .ForMember(dest => dest.Cities, opt => opt.Ignore());
            CreateMap<CountryEntity, CountryReadDto>().ForMember(dest => dest.CitysId, opt => opt.Ignore());
        }
    }
}
