using AutoMapper;
using Books.Api.Dtos;
using Books.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Api.Profiles
{
    public class BooksProfile: Profile
    {
        public BooksProfile()
        {
            CreateMap<Book, BookCreateDto>().ReverseMap();
            CreateMap<Book, BookUpdateDto>().ReverseMap();
        }
    }
}
