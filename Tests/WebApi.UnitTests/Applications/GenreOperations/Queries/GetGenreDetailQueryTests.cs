using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using Xunit;

namespace Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidIdIsGiven_Genre_ShouldBeReturned()
        {
            /*
                context.Genres.AddRange
                (
                    new Genre {Name = "Personal Growth"},
                    new Genre {Name = "Science Fiction"},
                    new Genre {Name = "Novel"}
                );
            */

            // Given
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            GenreDetailViewModel model = new GenreDetailViewModel();
            query.GenreId = 1;

            // When & Then
            var result = FluentActions.Invoking(() => query.Handle()).Invoke();

            result.Should().NotBeNull();
            result.Name.Should().Be("Personal Growth");
            result.Id.Should().Be(1);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            GenreDetailViewModel model = new GenreDetailViewModel();
            query.GenreId = 4;            
            // When & Then
            FluentActions.Invoking(() => query.Handle()).Should()
            .Throw<InvalidOperationException>().And.Message.Should().Be("Book type not found");
        }
    }
}