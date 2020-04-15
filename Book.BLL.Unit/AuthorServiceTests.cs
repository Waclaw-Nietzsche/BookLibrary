using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Core;
using Book.Core.Models;
using Book.Core.Repositories;
using Book.Core.Services;
using Moq;
using Xunit;

namespace Book.BLL.Unit
{
    public class AuthorServiceTests
    {
        public (Mock<IUnitOfWork> unitOfWork, Mock<IAuthorRepository> authorRepository, Dictionary<int, AuthorModel> context) GetMock()
        {
            Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            Mock<IAuthorRepository> authorRepository = new Mock<IAuthorRepository>(MockBehavior.Strict);

            Dictionary<int, AuthorModel> context = new Dictionary<int, AuthorModel>
            {
                [1] = new AuthorModel
                {
                    Name = "Skopina",
                    Id = 1,
                },
                [2] = new AuthorModel
                {
                    Name = "Bernard",
                    Id = 2,
                },
                [3] = new AuthorModel
                {
                    Name = "Newton",
                    Id = 3,
                }
            };

            unitOfWork.SetupGet(e => e.Authors).Returns(authorRepository.Object);
            unitOfWork.Setup(e => e.CommitAsync()).ReturnsAsync(0);

            authorRepository.Setup(e => e.ExistsAuthor(It.IsAny<int>())).ReturnsAsync((int id) => context.ContainsKey(id));
            authorRepository.Setup(e => e.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => context[id]);

            return (unitOfWork, authorRepository, context);

        }

        [Fact]
        public async Task CreateAuthor_Success()
        {
            // Arrange
            var (unitOfWork, authorRepository, context) = GetMock();
            authorRepository.Setup(e => e.AddAsync(It.IsAny<AuthorModel>()))
                .Callback((AuthorModel author) => { context.Add(4, author); }).Returns((AuthorModel _) => Task.CompletedTask);

            var author = new AuthorModel
            {
                Name = "Lunacharskiy"
            };
            var authorService = new AuthorService(unitOfWork.Object);

            // Act
            await authorService.CreateAuthor(author);
            
            // Assert
            Assert.True(context.ContainsKey(4));

        }

        [Fact]
        public async Task CreateAuthor_Failed()
        {
            // Arrange
            var (unitOfWork, authorRepository, context) = GetMock();
            authorRepository.Setup(e => e.AddAsync(It.IsAny<AuthorModel>()))
                .Callback((AuthorModel author) => { context.Add(5, null); }).Throws<NullReferenceException>();

            AuthorModel author = null;
            var authorService = new AuthorService(unitOfWork.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => authorService.CreateAuthor(author));
        }
        
        [Fact]
        public async Task DeleteAuthor_Success()
        {
            // Arrange
            var (unitOfWork, authorRepository, context) = GetMock();
            authorRepository.Setup(e => e.Remove(It.IsAny<AuthorModel>()))
                .Callback((AuthorModel author) => { context.Remove(author.Id); });

            var author = new AuthorModel
            {
                Id = 3,
                Name = "Newton"
            };
            var authorService = new AuthorService(unitOfWork.Object);

            // Act
            await authorService.DeleteAuthor(author);
            
            // Assert
            Assert.False(context.ContainsKey(3));

        }
        
        [Fact]
        public async Task UpdateAuthor_Success()
        {
            // Arrange
            var (unitOfWork, authorRepository, context)  = GetMock();
            authorRepository.Setup(e => e.GetWithBooksByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => context[id]);
            
            var author = new AuthorModel
            {
                Name = "Kapiza"
            };
            var authorService = new AuthorService(unitOfWork.Object);
        
            // Act
            await authorService.UpdateAuthor(3, author);
            
            // Assert
            Assert.Equal((await unitOfWork.Object.Authors.GetByIdAsync(3)).Name, author.Name);
        }
        
        [Fact]
        public async Task UpdateAuthor_Failed()
        {
            // Arrange
            var (unitOfWork, authorRepository, context)  = GetMock();
            authorRepository.Setup(e => e.GetWithBooksByIdAsync(It.IsAny<int>()))
                .Throws<NullReferenceException>();
            
            var author = new AuthorModel
            {
                Name = ""
            };
            var authorService = new AuthorService(unitOfWork.Object);
            
            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => authorService.UpdateAuthor(4, author));
        }
    }
}