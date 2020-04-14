using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Core.Models;

namespace Book.Core.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookModel>> GetAllWithAuthor();
        Task<BookModel> GetBookById(int id);
        Task<IEnumerable<BookModel>> GetBooksByAuthorId(int authorId);
        Task<BookModel> CreateBook(BookModel newBook);
        Task UpdateBook(int id, BookModel book);
        Task DeleteBook(BookModel book);
    }
}