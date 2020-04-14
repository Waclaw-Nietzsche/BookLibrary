using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Book.Core;
using Book.Core.Models;
using Moq;
using Xunit; 

namespace Book.BLL.Unit
{
    public class BookServiceTests
    {
        [Fact]
        public async Task CreateBook_NoObject()
        {
            // Arrange
            var id = 0;
            var book = new BookModel();
            var unitOfWork = new Mock<IUnitOfWork>();
            
            var service = new BookService(unitOfWork.Object);
            
            // Act
            book = null;
            var result = await service.CreateBook(book);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.CreateBook(book));
        }
        
        [Fact]
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