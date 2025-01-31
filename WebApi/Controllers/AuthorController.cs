using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthorCommand;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthorCommand;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Queries.GetAuthorsDetail;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class AuthorController:ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public AuthorController (BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult GetAuthorsDetail(int id)
        {
            GetAuthorsDetailQuery query = new GetAuthorsDetailQuery(_context, _mapper);
            query.AuthorId = id;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = newAuthor;

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = id;
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updateAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = id;
            command.Model = updateAuthor;  
            
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);    
            command.Handle();          

            return Ok();
        }
    }
}