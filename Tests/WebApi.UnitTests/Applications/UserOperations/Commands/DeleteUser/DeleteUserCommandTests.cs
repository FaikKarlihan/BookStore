using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.DeleteUser;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteUserCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenValidIdIsGiven_User_ShouldBeDeleted()
        {
            // Given
            var user = new User
            {
                Name = "Jesse",
                Surname = "Pinkman",
                Email = "jesse@outlook.com",
                Password = "777555"                
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            
            DeleteUserCommand command = new DeleteUserCommand(_context);
            command.userId = user.Id;

            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then

            var result = _context.Users.SingleOrDefault(x=> x.Id == user.Id);
            
            result.Should().BeNull();
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            int maxid = _context.Users.Count();
            DeleteUserCommand command = new DeleteUserCommand(_context);
            command.userId = maxid+99;
            // When & Then 
            FluentActions.Invoking(()=> command.Handle()).Should()
            .Throw<InvalidOperationException>("User is not found");
        }
    }
}