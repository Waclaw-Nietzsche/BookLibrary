using System;
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
        public async Task UpdateBook_ArgumentNullException()
        {
            // Arrange
            var book = new BookModel();
            var bookToBeUpdated = new BookModel();
            var unitOfWork = new Mock<IUnitOfWork>();

            var service = new BookService(unitOfWork.Object);
            
            // Act
            book = null;
            
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateBook(bookToBeUpdated, book));
        }
        
        [Fact]
        public void UpdateBook_Success()
        {
            // Arrange
            var book = new BookModel
            {
                AuthorId = 1,
                Name = "Mathematics"
            };
            var bookToBeUpdated = new BookModel
            {
                AuthorId = 2,
                Name = "History"
            };
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.CommitAsync());

            var bookData = new BookService(unitOfWork.Object);
            
            // Act
            var result = bookData.UpdateBook(bookToBeUpdated, book);

            // Assert
            Assert.Equal(bookToBeUpdated.Name, book.Name);
            Assert.Equal(bookToBeUpdated.AuthorId, book.AuthorId);
        }
    }
}