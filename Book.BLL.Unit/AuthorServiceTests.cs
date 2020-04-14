using System;
using System.Threading.Tasks;
using Book.Core;
using Book.Core.Models;
using Book.Core.Services;
using Moq;
using Xunit;

namespace Book.BLL.Unit
{
    public class AuthorServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        
        
        //[Fact]
        public void CreateAuthor_NoObjectTaskCompleted()
        {
            // Arrange
            var author = new AuthorModel();
            var service = new AuthorService(_unitOfWork.Object);
            TaskStatus status = TaskStatus.RanToCompletion;

            // Act
            author = null;
            var result = service.CreateAuthor(author);

            // Assert
            Assert.Equal(status, result.Status);
        }
        
        //[Fact]
        public async void GetAuthorById_Task_Return_OkResult()  
        {  
            //Arrange  
            Mock<IAuthorService> mock = new Mock<IAuthorService>();
            var Id = 1;  
            
            //Act  
            var data = mock.Object.GetAuthorById(Id);;
  
            //Assert  
            await Assert.IsType<Task<AuthorModel>>(data);  
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