using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.UpdateUser;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateUserCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
    
        [Fact]
        public void WhenValidIdIsGiven_User_ShouldBeUpdated()
        {
            // Given
            var user = new User
            {
                Name = "Rick",
                Surname = "Sanchez",
                Email = "ricky@outlook.com",
                Password = "000111"                
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            UpdateUserCommand command = new UpdateUserCommand(_context, _mapper);
            UpdateUserModel model = new UpdateUserModel()
            {
                Name = "Morty",
                Surname = "Smith",
                Email = "evilmorty@outlook.com",
                Password = "111000"
            };
            command.Model = model;
            command.userId = user.Id;

            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then

            var result = _context.Users.SingleOrDefault(x=> x.Id == user.Id);

            result.Name.Should().Be(model.Name);
            result.Surname.Should().Be(model.Surname);
            result.Password.Should().Be(model.Password);
            result.Email.Should().Be(model.Email);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            int maxid = _context.Users.Count();
            UpdateUserCommand command = new UpdateUserCommand(_context, _mapper);
            command.userId = maxid+99;
            // When & Then 
            FluentActions.Invoking(()=> command.Handle()).Should()
            .Throw<InvalidOperationException>("User is not found");
        }

        [Fact]
        public void WhenThereISaUserOnTheSameEmail_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            var user = new User
            {
                Id = 99,
                Name = "Jesse",
                Surname = "Pinkman",
                Email = "jesse@outlook.com",
                Password = "777555"                
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            UpdateUserCommand command = new UpdateUserCommand(_context, _mapper);
            UpdateUserModel model = new UpdateUserModel(){
                Name = "Jes",
                Surname = "Pink",
                Email = "jesse@outlook.com",
                Password = "777555"
            };
            command.Model = model;  
            command.userId = 1;

            // When & Then
            FluentActions.Invoking(()=> command.Handle()).Should()
            .Throw<InvalidOperationException>("There is a user with the same email");
        }
    }
}