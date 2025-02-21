using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorsDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidIdIsGiven_Author_ShouldBeReturn()
        {
            // Given
            var author = new Author()
            {
                Id = 222,
                Name = "Hayko",
                Surname = "Cepkin",
                BirthDate = DateTime.Now.Date.AddYears(-2),
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            GetAuthorsDetailQuery query = new GetAuthorsDetailQuery(_context, _mapper);
            GetAuthorsDetailQueryViewModel model = new GetAuthorsDetailQueryViewModel();
            query.AuthorId = 222;
            // When
            FluentActions.Invoking(()=> query.Handle()).Invoke();
            // Then
            var result = _context.Authors.SingleOrDefault(x=> x.Id == 222);

            result.Name.Should().Be("Hayko");  
            result.Surname.Should().Be("Cepkin");           
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            var author = new Author()
            {
                Id = 15,
                Name = "Hayko",
                Surname = "Cepkin",
                BirthDate = DateTime.Now.Date.AddYears(-2),
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            GetAuthorsDetailQuery query = new GetAuthorsDetailQuery(_context, _mapper);
            GetAuthorsDetailQueryViewModel model = new GetAuthorsDetailQueryViewModel();
            query.AuthorId = 999;
            // When
            FluentActions.Invoking(()=> query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author not found");           
        }
    }
}