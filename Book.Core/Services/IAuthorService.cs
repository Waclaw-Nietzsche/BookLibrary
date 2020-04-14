using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Core.Models;

namespace Book.Core.Services
{
    public interface IAuthorService
    {
         Task<IEnumerable<AuthorModel>> GetAllAuthors();
         Task<AuthorModel> GetAuthorById(int id);
         Task<AuthorModel> CreateAuthor(AuthorModel newAuthor);
         Task UpdateAuthor(int id, AuthorModel author);
         Task DeleteAuthor(AuthorModel author);
     }
 }