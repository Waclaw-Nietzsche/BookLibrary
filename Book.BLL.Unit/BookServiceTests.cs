using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Book.Core;
using Book.Core.Models;
using Book.Core.Repositories;
using Book.Core.Services;
using Book.DAL;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Book.BLL.Unit
{
    public class BookServiceTests
    {
        public (Mock<IUnitOfWork> unitOfWork, Mock<IBookRepository> bookRepository, Dictionary<int, BookModel> context) GetMock()
        {
            Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            Mock<IBookRepository> bookRepository = new Mock<IBookRepository>(MockBehavior.Strict);

            Dictionary<int, BookModel> context = new Dictionary<int, BookModel>
            {
                [1] = new BookModel
                {
                    Name = "Island of treasures",
                    AuthorId = 1,
                },
                [2] = new BookModel
                {
                    Name = "Spy story",
                    AuthorId = 2,
                },
                [3] = new BookModel
                {
                    Name = "Tragicomedy",
                    AuthorId = 3,
                }
            };

            unitOfWork.SetupGet(e => e.Books).Returns(bookRepository.Object);
            unitOfWork.Setup(e => e.CommitAsync()).ReturnsAsync(0);

            bookRepository.Setup(e => e.ExistsBook(It.IsAny<int>())).ReturnsAsync((int id) => context.ContainsKey(id));
            bookRepository.Setup(e => e.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => context[id]);

            return (unitOfWork, bookRepository, context);

        }

        [Fact]
        public async Task CreateBook_Success()
        {
            // Arrange
            var (unitOfWork, bookRepository, context) = GetMock();
            bookRepository.Setup(e => e.AddAsync(It.IsAny<BookModel>()))
                .Callback((BookModel book) => { context.Add(4, book); }).Returns((BookModel _) => Task.CompletedTask);

            var book = new BookModel
            {
                Name = "Biography"
            };
            var bookService = new BookService(unitOfWork.Object);

            // Act
            await bookService.CreateBook(book);
            
            // Assert
            Assert.True(context.ContainsKey(4));

        }

        [Fact]
        public async Task CreateBook_Failed()
        {
            // Arrange
            var (unitOfWork, bookRepository, context) = GetMock();
            bookRepository.Setup(e => e.AddAsync(It.IsAny<BookModel>()))
                .Callback((BookModel book) => { context.Add(5, null); }).Throws<NullReferenceException>();

            BookModel book = null;
            var bookService = new BookService(unitOfWork.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => bookService.CreateBook(book));
        }
        
        [Fact]
        public async Task DeleteBook_Success()
        {
            // Arrange
            var (unitOfWork, bookRepository, context) = GetMock();
            bookRepository.Setup(e => e.Remove(It.IsAny<BookModel>()))
                .Callback((BookModel book) => { context.Remove(book.Id); });

            var book = new BookModel
            {
                Id = 3,
                Name = "Tragicomedy"
            };
            var bookService = new BookService(unitOfWork.Object);

            // Act
            await bookService.DeleteBook(book);
            
            // Assert
            Assert.False(context.ContainsKey(3));

        }
        
        [Fact]
        public async Task UpdateBook_Success()
        {
            // Arrange
            var (unitOfWork, bookRepository, context)  = GetMock();
            bookRepository.Setup(e => e.GetWithAuthorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => context[id]);
            
            var book = new BookModel
            {
                Name = "Monography"
            };
            var bookService = new BookService(unitOfWork.Object);
        
            // Act
            await bookService.UpdateBook(3, book);
            
            // Assert
            Assert.Equal((await unitOfWork.Object.Books.GetByIdAsync(3)).Name, book.Name);
        }
        
        [Fact]
        public async Task UpdateBook_Failed()
        {
            // Arrange
            var (unitOfWork, bookRepository, context)  = GetMock();
            bookRepository.Setup(e => e.GetWithAuthorByIdAsync(It.IsAny<int>()))
                .Throws<NullReferenceException>();
            
            var book = new BookModel
            {
                Name = ""
            };
            var bookService = new BookService(unitOfWork.Object);
            
            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => bookService.UpdateBook(4, book));
        }
        
    }
}