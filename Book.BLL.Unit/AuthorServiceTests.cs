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
        public void CreateAuthor_NoObject()
        {
            // Arrange
            var id = 0;
            var author = new AuthorModel();
            var unitOfWork = new Mock<IUnitOfWork>();
            
            var service = new AuthorService(unitOfWork.Object);
            
            // Act
            author = null;
            var result = service.CreateAuthor(author);

            // Assert
            Assert.Null(service.CreateAuthor(author));
        }
        
        //[Fact]
        public async Task UpdateAuthor_ArgumentNullException()
        {
            // Arrange
            var id = 0;
            var author = new AuthorModel();
            var unitOfWork = new Mock<IUnitOfWork>();

            var service = new AuthorService(unitOfWork.Object);
            
            // Act
            author = null;
            
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateAuthor(id, author));
        }
        
        //[Fact]
        public void UpdateAuthor_Success()
        {
            // Arrange
            var id = 1;
            var author = new AuthorModel
            {
                Name = "Ivan",
                Id = 1
            };
            var authorToBeUpdated = new AuthorModel
            {
                Name = "Pyotr",
                Id = 2
            };
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.CommitAsync());

            var bookData = new AuthorService(unitOfWork.Object);
            
            // Act
            var result = bookData.UpdateAuthor(id, author);

            // Assert
            Assert.Equal(authorToBeUpdated.Name, author.Name);
        }
    }
}