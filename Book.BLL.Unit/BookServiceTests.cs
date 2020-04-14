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

namespace Book.BLL.Unit
{
    public class BookServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private readonly ITestOutputHelper _testOutputHelper;
        
        public BookServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void CreateBook_NoObjectTaskCompleted()
        {
            // Arrange
            var book = new BookModel();
            var service = new BookService(_unitOfWork.Object);
            TaskStatus status = TaskStatus.RanToCompletion;

            // Act
            book = null;
            var result = service.CreateBook(book);

            // Assert
            Assert.Equal(status, result.Status);
        }
        
        [Fact]
        public async void GetBookById_Task_Return_OkResult()  
        {  
            //Arrange  
            Mock<IBookService> mock = new Mock<IBookService>();
            var Id = 1;  
            
            //Act  
            var data = mock.Object.GetBookById(Id);
  
            //Assert  
            await Assert.IsType<Task<BookModel>>(data);  
        }  
        
        [Fact]
        public async void GetAllWithAuthor_Task_Return_OkResult()  
        {  
            //Arrange  
            Mock<IBookService> mock = new Mock<IBookService>();

            //Act  
            var data = mock.Object.GetAllWithAuthor();
  
            //Assert  
            await Assert.IsType<Task<IEnumerable<BookModel>>>(data);  
        }
        
        [Fact]
        public async void GetBooksByAuthorId_Task_Return_OkResult()  
        {
            //Arrange  
            Mock<IBookService> mock = new Mock<IBookService>();
            var authorId = 1;  
            
            //Act  
            var data = mock.Object.GetBooksByAuthorId(authorId);
  
            //Assert  
            await Assert.IsType<Task<IEnumerable<BookModel>>>(data);  
        }
        
        //[Fact]
        public void UpdateBook_Success()
        {
            // Arrange
            var book = new BookModel
            {
                AuthorId = 1,
                Name = "Mathematics",
                Id = 1
            };
            var bookToBeUpdated = new BookModel
            {
                AuthorId = 2,
                Name = "History",
                Id = 2
            };
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.CommitAsync());

            var bookData = new BookService(unitOfWork.Object);

            // Act
            var result = bookData.UpdateBook(bookToBeUpdated.Id, book);

            // Assert
            Assert.Equal(bookToBeUpdated.Name, book.Name);
            Assert.Equal(bookToBeUpdated.AuthorId, book.AuthorId);
        }
    }
}