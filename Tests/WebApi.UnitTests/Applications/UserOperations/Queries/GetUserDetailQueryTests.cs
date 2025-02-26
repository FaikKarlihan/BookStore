using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Queries.GetUserDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.UserOperations.Queries.GetUserDetail
{
    public class GetUserDetailQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetUserDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidIdIsGiven_User_ShouldBeReturned()
        {
            // Given
            var iuser = new User
            {
                Id = 999,
                Name="dadada",
                Surname = "adadad",
                Email = "fafafa",
                Password = "rarara"
            };
            _context.Users.Add(iuser);
            _context.SaveChanges();

            GetUserDetailQuery query = new GetUserDetailQuery(_context, _mapper);
            query.userId = iuser.Id;
            // When
            FluentActions.Invoking(()=> query.Handle()).Invoke();
            // Then
            var user = _context.Users.SingleOrDefault(x=> x.Id == iuser.Id);

            user.Should().NotBeNull();
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            GetUserDetailQuery query = new GetUserDetailQuery(_context, _mapper);
            query.userId = 999;
            // When & Then
            FluentActions.Invoking(()=> query.Handle()).Should()
            .Throw<InvalidOperationException>("User not found");
        }
    }
}