using AutoMapper;
using Books.Application.DTOs.CityDTOs;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Mapping
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<CityEntity, CityReadDto>();
        }
    }
}
