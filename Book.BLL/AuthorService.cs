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
            this._unitOfWork = unitOfWork;
        }

        public async Task<AuthorModel> CreateAuthor(AuthorModel newAuthor)
        {
            await _unitOfWork.Authors
                .AddAsync(newAuthor);

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

        public async Task UpdateAuthor(AuthorModel authorToBeUpdated, AuthorModel author)
        {
            if ((authorToBeUpdated == null) || (author == null))
            {
                throw new ArgumentNullException(nameof(authorToBeUpdated));
            }
            
            authorToBeUpdated.Name = author.Name;

            await _unitOfWork.CommitAsync();
        }
    }
}