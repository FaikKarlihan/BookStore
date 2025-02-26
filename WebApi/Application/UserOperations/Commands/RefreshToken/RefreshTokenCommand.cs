using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Model;

namespace WebApi.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string refreshToken {get;set;}
        private readonly IBookStoreDbContext _context;
        private readonly IConfiguration _configuration;
        public RefreshTokenCommand(IBookStoreDbContext dbContext, IConfiguration configuration)
        {
            _context = dbContext;
            _configuration = configuration;
        }
        public Token Handle()
        {
            var user = _context.Users.
            FirstOrDefault(x=> x.RefreshToken == refreshToken && x.RefreshTokenExpireDate > DateTime.Now);
            if(user is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _context.SaveChanges();
                
                return token;
            }
            else
                throw new InvalidOperationException ("No valid refresh token found");
        }
    }
}