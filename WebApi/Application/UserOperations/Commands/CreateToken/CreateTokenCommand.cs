using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Model;

namespace WebApi.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model {get;set;}
        private readonly IBookStoreDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public CreateTokenCommand(IBookStoreDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public Token Handle()
        {
            var user = _dbContext.Users.
            FirstOrDefault(x=> x.Email == Model.Email && x.Password == Model.Password);
            if(user is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _dbContext.SaveChanges();
                
                return token;
            }
            else
                throw new InvalidOperationException ("Username - Password is incorrect");
        }

        public class CreateTokenModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}