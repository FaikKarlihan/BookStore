using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper =testFixture.Mapper;
        }
        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            int maxId = _context.Books.Max(b => (int?)b.Id) ?? 0;
            int invalidId = maxId + 1;

            GetBookDetailQuery query = new GetBookDetailQuery(_context, null);
            query.BookId = invalidId;


            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Book is not found");
        }

        [Fact]
        public void WhenValidIdIsGiven_Book_ShouldBeReturned()
        {
            var book = new Book
            {
                Title = "Test Book",
                GenreId = 1,
                PageCount = 100,
                PublishDate = new DateTime(2000, 1, 1),
                AuthorId = 1
            };
            
            _context.Books.Add(book);
            _context.SaveChanges();
        
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = book.Id;
        
            var result = FluentActions.Invoking(() => query.Handle()).Invoke();

            result.Should().NotBeNull();
            result.title.Should().Be(book.Title);
            result.pageCount.Should().Be(book.PageCount);
        }
    }
}