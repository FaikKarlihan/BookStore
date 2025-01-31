using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _DbContex;
        private readonly IMapper _mapper;
        public int BookId {get;set;}
        public int maxId {get;set;}
        public GetBookDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _DbContex = context;
            _mapper = mapper;
            maxId = _DbContex.Books.Any()
                ? _DbContex.Books.Max(book => book.Id)
                : 0;
        }

        public BookDetailViewModel Handle()
        {
            var book = _DbContex.Books.Include(x=> x.Genre).Where(book=> book.Id == BookId).SingleOrDefault();
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
        }
    
}

