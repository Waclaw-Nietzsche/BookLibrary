﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Core.Models;

namespace Book.Core.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookModel>> GetAllWithAuthor();
        Task<BookModel> GetBookBuId(int id);
        Task<IEnumerable<BookModel>> GetBooksByAuthorId(int authorId);
        Task<BookModel> CreateBook(BookModel newBook);
        Task UpdateBook(BookModel bookToBeUpdated, BookModel book);
        Task DeleteBook(BookModel book);
    }
}