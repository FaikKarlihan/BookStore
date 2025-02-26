using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommand
    {
        private readonly IBookStoreDbContext _context;
        public int userId { get; set; }

        public DeleteUserCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var user =  _context.Users.SingleOrDefault(x=> x.Id == userId);
            if(user is null)
                throw new InvalidOperationException("User is not found");
            
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}