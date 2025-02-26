using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Application.UserOperations.Commands.CreateToken;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.Application.UserOperations.Commands.DeleteUser;
using WebApi.Application.UserOperations.Commands.RefreshToken;
using WebApi.Application.UserOperations.Commands.UpdateUser;
using WebApi.Application.UserOperations.Queries.GetUserDetail;
using WebApi.Application.UserOperations.Queries.GetUsers;
using WebApi.DBOperations;
using WebApi.TokenOperations.Model;
using static WebApi.Application.UserOperations.Commands.CreateToken.CreateTokenCommand;
using static WebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class UserController:ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration;
        public UserController(IBookStoreDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            command.Model = newUser;
            command.Handle();

            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context, _configuration);
            command.Model = login;

            var token = command.Handle();
            return token;
        }

        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
            command.refreshToken = token;

            var resultToken = command.Handle();
            return resultToken;
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateUserModel updateModel)
        {
            UpdateUserCommand command = new UpdateUserCommand(_context, _mapper);
            command.userId = id;
            command.Model = updateModel;  
            
            UpdateUserCommandValidator validator = new UpdateUserCommandValidator();
            validator.ValidateAndThrow(command);    
            command.Handle();          

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            DeleteUserCommand command = new DeleteUserCommand(_context);
            command.userId = id;
            DeleteUserCommandValidator validator = new DeleteUserCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            GetUsersQuery query = new GetUsersQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult GetUserDetail(int id)
        {
            GetUserDetailQuery query = new GetUserDetailQuery(_context, _mapper);
            query.userId = id;

            GetUserDetailQueryValidator validator = new GetUserDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var obj = query.Handle();
            return Ok(obj);
        }
    }
}