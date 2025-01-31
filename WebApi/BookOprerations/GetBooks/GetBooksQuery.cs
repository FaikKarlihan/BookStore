using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBookQuery
    {
        private readonly BookStoreDbContext  _dbContext;
        private readonly IMapper _mapper;
        public GetBookQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<BookViewModel >Handler()
        {
            var bookList = _dbContext.Books.Include(x=> x.Genre).OrderBy(x=> x.Id).ToList<Book>();
            List<BookViewModel> vm = _mapper.Map<List<BookViewModel>>(bookList);//new List<BookViewModel>();
            // foreach (var book in bookList)
            // {
            //     vm.Add(new BookViewModel(){
            //         Title = book.Title,
            //         Genre = ((GenreEnum)book.GenreId).ToString(),
            //         PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
            //         PageCount = book.PageCount 
            //     });
            // }
            return vm;
        }

        public class BookViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string  PublishDate { get; set; }
            public string Genre { get; set; }
        }
    }
}
