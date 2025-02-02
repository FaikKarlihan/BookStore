using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly BookStoreDbContext _DbContext;
        private readonly IMapper _Mapper;
        public GetAuthorsQuery(BookStoreDbContext contex, IMapper mapper)
        {
            _DbContext = contex;
            _Mapper = mapper;
        }
        public List<GetAuthorsQueryViewModel> Handle()
        {
            var authors = _DbContext.Authors.Include(x=> x.Books).OrderBy(x=> x.Id).ToList();;
            List<GetAuthorsQueryViewModel> vm = _Mapper.Map<List<GetAuthorsQueryViewModel>>(authors);
            return vm;
        }
    }
    public class GetAuthorsQueryViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
        public List<string> Books { get; set; }
    }
}