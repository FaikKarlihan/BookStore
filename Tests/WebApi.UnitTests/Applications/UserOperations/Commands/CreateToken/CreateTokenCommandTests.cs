using System;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using TestSetup;
using WebApi.Application.UserOperations.Commands.CreateToken;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.UserOperations.Commands.CreateToken.CreateTokenCommand;

namespace Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IConfiguration _configuration;

        public CreateTokenCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _configuration = testFixture.Configuration;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Token_ShouldBeCreated()
        {
            // Given
            var user = new User
            {
                Name = "Tyrion ",
                Surname = "Lannister",
                Email = "lann@gmail.com",
                Password = "123456" 
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            CreateTokenCommand command = new CreateTokenCommand(_context, _configuration);
            CreateTokenModel model = new CreateTokenModel()
            {
                Email = "lann@gmail.com",
                Password = "123456"
            };
            command.Model = model;
            // When
            var token = FluentActions.Invoking(() => command.Handle()).Invoke();
            // Then
            token.AccessToken.Should().NotBeNull();
            token.RefreshToken.Should().NotBeNull();
            token.Expiration.Should().BeAfter(DateTime.Now);
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            CreateTokenCommand command = new CreateTokenCommand(_context, _configuration);
            CreateTokenModel model = new CreateTokenModel()
            {
                Email = "balabla@gamail.com",
                Password = "onetwo"
            };
            command.Model = model;
            // When
            FluentActions.Invoking(() => command.Handle()).Should()
            .Throw<InvalidOperationException>("Username - Password is incorrect");
        }
    }
}