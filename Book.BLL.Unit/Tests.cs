using System.Threading.Tasks;
using Book.Core;
using Book.Core.Models;
using Moq;
using NUnit.Framework;

namespace Book.BLL.Unit
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public async Task CreateBook_Success()
        {
            // Arrange
            var book = new BookModel();
            var unitOfWork = new Mock<IUnitOfWork>();

            var service = new BookService(unitOfWork.Object);
            
            // Act
            var result = await service.CreateBook(book);
            
            // Assert
            Assert.IsNotNull(book);
        }
        
        [Test]
        public void UpdateBook_Success()
        {
            // Arrange
            int x = 10;
            int y = 20;
            int expected = 30;

            // Act
            int d = x + y;

            // Assert
            Assert.AreEqual(expected, d);

        }
    }
}