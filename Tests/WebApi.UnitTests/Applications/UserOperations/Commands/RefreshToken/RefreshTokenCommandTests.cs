using System;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using TestSetup;
using WebApi.Application.UserOperations.Commands.CreateToken;
using WebApi.Application.UserOperations.Commands.RefreshToken;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.UserOperations.Commands.CreateToken.CreateTokenCommand;

namespace Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommandTests(CommonTestFixture testFixture)
        {
            _configuration = testFixture.Configuration;
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenValidRefreshTokenIsGiven_NewToken_ShouldBeCreated()
        {
            // Given
            var user = new User
            {
                Name = "Arya",
                Surname = "Stark",
                Email = "arya@gmail.com",
                Password = "123456" 
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            CreateTokenCommand command = new CreateTokenCommand(_context, _configuration);
            CreateTokenModel model = new CreateTokenModel()
            {
                Email = "arya@gmail.com",
                Password = "123456"
            };
            command.Model = model;
            // When
            var token = FluentActions.Invoking(() => command.Handle()).Invoke();

            
            RefreshTokenCommand commandRefresh = new RefreshTokenCommand(_context, _configuration);
            commandRefresh.refreshToken = token.RefreshToken;
            // When
            var newToken = FluentActions.Invoking(()=> commandRefresh.Handle()).Invoke();
            // Then
            newToken.AccessToken.Should().NotBeNull();
            newToken.RefreshToken.Should().NotBeNull();
            newToken.Expiration.Should().BeAfter(DateTime.Now);
        }

        [Fact]
        public void WhenInvalidRefreshTokenIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
            command.refreshToken = "invalid refresh token";
            // When & Then
            FluentActions.Invoking(()=> command.Handle()).Should()
            .Throw<InvalidOperationException>("No valid refresh token found");
        }
    }
}