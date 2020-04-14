using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Book.API.Resources;
using Book.API.Validators;
using Book.Core.Models;
using Book.Core.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Book.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<SaveBookResource> _validator;

        public BooksController(IBookService bookService, IMapper mapper, AbstractValidator<SaveBookResource> validator)
        {
            _mapper = mapper;
            _bookService = bookService;
            _validator = validator;

        }
        
        [HttpGet("")]
        public async Task<OkObjectResult> GetAllBooks()
        {
            var books = await _bookService.GetAllWithAuthor();
            var bookResources = 
                _mapper.Map<IEnumerable<BookModel>, IEnumerable<BookResource>>(books);
            
            return Ok(bookResources);
        }

        [HttpGet("{id}")]
        public async Task<OkObjectResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookById(id);
            var bookResource = _mapper.Map<BookModel, BookResource>(book);

            return Ok(bookResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<BookResource>> CreateBook([FromBody] SaveBookResource saveBookResource)
        {
            var validationResult = await _validator.ValidateAsync(saveBookResource);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var bookToCreate = _mapper.Map<SaveBookResource, BookModel>(saveBookResource);

            var newBook = await _bookService.CreateBook(bookToCreate);
            
            var bookResource = _mapper.Map<BookModel, BookResource>(newBook);

            return Ok(bookResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookResource>> UpdateBook(int id, [FromBody] SaveBookResource saveBookResource)
        {
            var validationResult = await _validator.ValidateAsync(saveBookResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            var book = _mapper.Map<SaveBookResource, BookModel>(saveBookResource);

            await _bookService.UpdateBook(id, book);

            var updatedBook = await _bookService.GetBookById(id);
            var updatedBookResource = _mapper.Map<BookModel, BookResource>(updatedBook);

            return Ok(updatedBookResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var book = await _bookService.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            await _bookService.DeleteBook(book);

            return NoContent();
        }
    }
}