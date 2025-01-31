using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorsDetail
{
    public class GetAuthorsDetailQuery
    {
        private readonly BookStoreDbContext _DbContext;
        private readonly IMapper _Mapper;
        public int AuthorId { get; set; }
        public GetAuthorsDetailQuery(BookStoreDbContext contex, IMapper mapper)
        {
            _DbContext = contex;
            _Mapper = mapper;
        }
        public GetAuthorsDetailQueryViewModel  Handle()
        {
            var authors = _DbContext.Authors.Where(x=> x.Id == AuthorId).SingleOrDefault();
            if (authors is null)
                throw new InvalidOperationException("Author not found");
            
            GetAuthorsDetailQueryViewModel vm = _Mapper.Map<GetAuthorsDetailQueryViewModel>(authors);
            return vm;
        }
    }
    public class GetAuthorsDetailQueryViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
    }    
}