using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model { get; set; }
        private readonly BookStoreDbContext _DbContext;
        private readonly IMapper _Mapper;
        public CreateAuthorCommand(BookStoreDbContext contex, IMapper mapper)
        {
            _DbContext = contex;
            _Mapper = mapper;
        }
        public void Handle()
        {
            var author = _DbContext.Authors.SingleOrDefault(x=> x.Name == Model.Name && x.Surname == Model.Surname);
            if (author is not null)
                throw new InvalidOperationException("Author already exists");
            author = _Mapper.Map<Author>(Model);

            _DbContext.Authors.Add(author);
            _DbContext.SaveChanges();
        }

    }
    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}