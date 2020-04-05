using System.Threading.Tasks;
using Book.Core;
using Book.Core.Repositories;
using Book.DAL.Repositories;

namespace Book.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookDbContext _context;
        private BookRepository _bookRepository;
        private AuthorRepository _authorRepository;

        public UnitOfWork(BookDbContext context)
        {
            this._context = context;
        }

        public IBookRepository Books => _bookRepository ??= new BookRepository(_context);
        public IAuthorRepository Authors => _authorRepository ??= new AuthorRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}