using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Core.Models;
using Book.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Book.DAL.Repositories
{
    public class AuthorRepository : Repository<AuthorModel>, IAuthorRepository
    {
        public AuthorRepository(DbContext context) 
            : base(context)
        { }

        private BookDbContext BookDbContext => Context as BookDbContext;

        public async Task<IEnumerable<AuthorModel>> GetAllWithBooksAsync()
        {
            return await BookDbContext.Authors
                .Include(artmodel => artmodel.Books)
                .ToListAsync();
        }

        public async Task<AuthorModel> GetWithBooksByIdAsync(int id)
        {
            return await BookDbContext.Authors
                .Include(artmodel => artmodel.Books)
                .SingleOrDefaultAsync(artmodel => artmodel.Id == id);
        }

        public async Task<bool> ExistsAuthor(int id)
        {
            return await GetByIdAsync(id) != null;
        }
    }
}