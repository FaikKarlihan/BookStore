using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Commands.CreateBook;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using AutoMapper;
using FluentValidation;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class BookController:ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController (BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBookQuery query = new GetBookQuery(_context, _mapper);
            var result = query.Handler();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;

            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;
            GetBookDetailQueryCommandValidator validator = new GetBookDetailQueryCommandValidator(query.maxId);
            validator.ValidateAndThrow(query);
            result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = newBook;

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle(); 
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookViewModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;  
            
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);    
            command.Handle();          

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
} 