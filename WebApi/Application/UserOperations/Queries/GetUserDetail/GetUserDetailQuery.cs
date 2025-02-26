using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.UserOperations.Queries.GetUserDetail
{
    public class GetUserDetailQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int userId { get; set; }

        public GetUserDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetUserDetailQueryViewModel Handle()
        {
            var user = _context.Users.Where(x=> x.Id == userId).SingleOrDefault();
            if (user is null)
                throw new InvalidOperationException("User not found");
            
            GetUserDetailQueryViewModel vm = _mapper.Map<GetUserDetailQueryViewModel>(user);
            return vm;
        }
    }

    public class GetUserDetailQueryViewModel 
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}