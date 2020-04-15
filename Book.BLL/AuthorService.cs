using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Core;
using Book.Core.Models;
using Book.Core.Services;

namespace Book.BLL
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthorModel> CreateAuthor(AuthorModel newAuthor)
        {
            if (newAuthor == null)
            {
                throw new NullReferenceException();
            }
            
            await _unitOfWork.Authors.AddAsync(newAuthor);
            await _unitOfWork.CommitAsync();

            return newAuthor;
        }

        public async Task DeleteAuthor(AuthorModel author)
        {
            _unitOfWork.Authors.Remove(author);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<AuthorModel>> GetAllAuthors()
        {
            return await _unitOfWork.Authors.GetAllAsync();
        }

        public async Task<AuthorModel> GetAuthorById(int id)
        {
            return await _unitOfWork.Authors.GetByIdAsync(id);
        }

        public async Task UpdateAuthor(int id, AuthorModel author)
        {
            var authorToBeUpdated = await _unitOfWork.Authors.GetWithBooksByIdAsync(id);
            if (await _unitOfWork.Authors.ExistsAuthor(id))
            {
                authorToBeUpdated.Name = author.Name;
            }
            else
            {
                throw new NullReferenceException("Can't update non-existing author.");
            }
            await _unitOfWork.CommitAsync();
        }
        
    }
}