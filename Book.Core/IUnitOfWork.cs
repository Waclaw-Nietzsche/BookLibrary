using System;
using System.Threading.Tasks;
using Book.Core.Repositories;

namespace Book.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        IAuthorRepository Authors { get; }
        Task<int> CommitAsync();
    }
}