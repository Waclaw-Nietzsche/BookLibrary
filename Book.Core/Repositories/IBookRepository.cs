using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Core.Models;

namespace Book.Core.Repositories
{
    public interface IBookRepository : IRepository<BookModel>
    {
        Task<IEnumerable<BookModel>> GetAllWithAuthorAsync();
        Task<BookModel> GetWithAuthorByIdAsync(int id);
        Task<IEnumerable<BookModel>> GetAllWithAuthorByAuthorIdAsync(int authorId);
        Task<bool> ExistsBook(int id);
    }
}