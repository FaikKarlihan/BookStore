using System;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _DbContex;
        public int BookId {get;set;}
        public GetBookDetailQuery(BookStoreDbContext context)
        {
            _DbContex = context;
        }

        public BookDetailViewModel Handle()
        {
            var book = _DbContex.Books.Where(book=> book.Id == BookId).SingleOrDefault();
            if (book is null)
                throw new InvalidOperationException("Book is not found");
            
            BookDetailViewModel vm = new BookDetailViewModel();
            vm.title = book.Title;
            vm.pageCount = book.PageCount;
            vm.genre = ((GenreEnum)book.GenreId).ToString();
            vm.publishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");

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