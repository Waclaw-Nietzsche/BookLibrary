using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Core.Models;
using Book.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Book.DAL.Repositories
{
    public class BookRepository : Repository<BookModel>, IBookRepository
    {
        public BookRepository(DbContext context)
            : base(context)
        { }

        private BookDbContext BookDbContext => Context as BookDbContext;

        public async Task<IEnumerable<BookModel>> GetAllWithAuthorAsync()
        {
            return await BookDbContext.Books
                .Include(model => model.Author)
                .ToListAsync();
        }

        public async Task<BookModel> GetWithAuthorByIdAsync(int id)
        {
            return await BookDbContext.Books
                .Include(model => model.Author)
                .SingleOrDefaultAsync(model => model.Id == id);
        }

        public async Task<IEnumerable<BookModel>> GetAllWithAuthorByAuthorIdAsync(int authorId)
        {
            return await BookDbContext.Books
                .Include(model => model.Author)
                .Where(model => model.AuthorId == authorId)
                .ToListAsync();
        }

        public async Task<bool> ExistsBook(int id)
        {
            return await GetByIdAsync(id) != null;
        }
    }
}