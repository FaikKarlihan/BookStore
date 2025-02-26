using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommand
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int userId { get; set; }
        public UpdateUserModel Model;

        public UpdateUserCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(x=> x.Id == userId);
            if(user is null)
                throw new InvalidOperationException("User is not found");
            if (_context.Users.Any(x=> x.Email == Model.Email.ToLower() && x.Id != userId))
                throw new InvalidOperationException("There is a user with the same email");

            _mapper.Map(Model, user);
            _context.SaveChanges();
        }
    }
    public class UpdateUserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}