using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Book.Core.Models;
using Book.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Book.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            this._bookService = bookService;
        }
        
        [HttpGet("")]
        public async Task<OkObjectResult> GetAllBooks()
        {
            var books = await _bookService.GetAllWithAuthor();
            return Ok(books);
        }
    }
}