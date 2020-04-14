using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Core;
using Book.Core.Models;
using Book.Core.Services;

namespace Book.BLL
 {
     public class BookService : IBookService
     {
         private readonly IUnitOfWork _unitOfWork;
 
         public BookService(IUnitOfWork unitOfWork)
         {
             this._unitOfWork = unitOfWork;
         }
 
         public async Task<BookModel> CreateBook(BookModel newBook)
         {
             if (ExistsBook(newBook))
             {
                 await _unitOfWork.Books.AddAsync(newBook);
                 await _unitOfWork.CommitAsync();
             }
             
             return newBook;
         }
 
         public async Task DeleteBook(BookModel book)
         {
             _unitOfWork.Books.Remove(book);
             await _unitOfWork.CommitAsync();
         }
 
         public async Task<IEnumerable<BookModel>> GetAllWithAuthor()
         {
             return await _unitOfWork.Books
                 .GetAllWithAuthorAsync();
         }
 
         public async Task<BookModel> GetBookById(int id)
         {
             return await _unitOfWork.Books
                 .GetWithAuthorByIdAsync(id);
         }
 
         public async Task<IEnumerable<BookModel>> GetBooksByAuthorId(int authorId)
         {
             return await _unitOfWork.Books
                 .GetAllWithAuthorByAuthorIdAsync(authorId);
         }
 
         public async Task UpdateBook(int id, BookModel book)
         {
             var bookToBeUpdated = await _unitOfWork.Books.GetWithAuthorByIdAsync(id);
             if (ExistsBook(bookToBeUpdated))
             {
                 bookToBeUpdated.Name = book.Name;
                 bookToBeUpdated.AuthorId = book.AuthorId;
                 await _unitOfWork.CommitAsync();
             }
         }

         private bool ExistsBook(BookModel book)
         {
             return book != null;
         }
     }
 }