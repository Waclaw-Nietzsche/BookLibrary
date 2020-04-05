using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Book.API.Resources;
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
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            this._mapper = mapper;
            this._bookService = bookService;
        }
        
        [HttpGet("")]
        public async Task<OkObjectResult> GetAllBooks()
        {
            var books = await _bookService.GetAllWithAuthor();
            var bookResources = 
                _mapper.Map<IEnumerable<BookModel>, IEnumerable<BookResource>>(books);
            
            return Ok(bookResources);
        }
    }
}