using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidInputsAreGiven_User_ShouldBeCreated()
        {
            // Given
            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            CreateUserModel model = new CreateUserModel(){
                Name = "Walter",
                Surname = "White",
                Email = "walterwhite@outlook.com",
                Password = "999888"
            };
            command.Model = model;
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            var user = _context.Users.SingleOrDefault(x=> x.Email == model.Email);
            
            user.Should().NotBeNull();
            user.Name.Should().Be(model.Name);
            user.Surname.Should().Be(model.Surname);
            user.Password.Should().Be(model.Password);
        }

        [Fact]
        public void WhenThereISaUserOnTheSameEmail_InvalidOperationException_ShouldBeReturn()
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

            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            CreateUserModel model = new CreateUserModel(){
                Name = "Jes",
                Surname = "Pink",
                Email = "jesse@outlook.com",
                Password = "777555"
            };
            command.Model = model;  

            // When & Then
            FluentActions.Invoking(()=> command.Handle()).Should()
            .Throw<InvalidOperationException>("User already exists");
        }
    }
}