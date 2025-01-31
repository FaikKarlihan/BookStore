using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }
        public readonly BookStoreDbContext _contex;
        public readonly IMapper _mapper;
        public GetGenreDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _contex = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _contex.Genres.SingleOrDefault(x=> x.IsActive && x.Id == GenreId);
            if (genre is null)
                throw new InvalidOperationException("Book type not found");
            return _mapper.Map<GenreDetailViewModel>(genre);
        }
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    } 
}