using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Core.Models;

namespace Book.Core.Repositories
{
    public interface IAuthorRepository : IRepository<AuthorModel>
    {
        Task<IEnumerable<AuthorModel>> GetAllWithBooksAsync();
        Task<AuthorModel> GetWithBooksByIdAsync(int id);
    }
}