using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly IBookStoreDbContext _DbContex;
        private readonly IMapper _mapper;
        public int BookId {get;set;}
        public int maxId {get;set;}
        public GetBookDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _DbContex = context;
            _mapper = mapper;
            maxId = _DbContex != null ? GetMaxId() : 0;
        }
        public int GetMaxId()
        {
            return _DbContex != null && _DbContex.Books.Any()
                ? _DbContex.Books.Max(book => book.Id)
                : 0;
        }

        public BookDetailViewModel Handle()
        {
            var book = _DbContex.Books.Include(x=> x.Author).Include(x=> x.Genre).Where(book=> book.Id == BookId).SingleOrDefault();
            if (book is null)
                throw new InvalidOperationException("Book is not found");
            
            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);

            return vm;
        }
    }
        public class BookDetailViewModel
        {
            public string title { get; set; }
            public string genre { get; set; }
            public int pageCount { get; set; }
            public string publishDate { get; set; }
            public string AuthorName { get; set; }
        }
    
}

