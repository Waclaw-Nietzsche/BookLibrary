using System;
using System.Threading.Tasks;
using Book.Core;
using Book.Core.Models;
using Moq;
using Xunit;

namespace Book.BLL.Unit
{
    public class AuthorServiceTests
    {
        [Fact]
        public async Task UpdateAuthor_ArgumentNullException()
        {
            // Arrange
            var author = new AuthorModel();
            var authorToBeUpdated = new AuthorModel();
            var unitOfWork = new Mock<IUnitOfWork>();

            var service = new AuthorService(unitOfWork.Object);
            
            // Act
            author = null;
            
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateAuthor(authorToBeUpdated, author));
        }
        
        [Fact]
        public void UpdateAuthor_Success()
        {
            // Arrange
            var author = new AuthorModel
            {
                Name = "Ivan"
            };
            var authorToBeUpdated = new AuthorModel
            {
                Name = "Pyotr"
            };
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.CommitAsync());

            var bookData = new AuthorService(unitOfWork.Object);
            
            // Act
            var result = bookData.UpdateAuthor(authorToBeUpdated, author);

            // Assert
            Assert.Equal(authorToBeUpdated.Name, author.Name);
        }
    }
}