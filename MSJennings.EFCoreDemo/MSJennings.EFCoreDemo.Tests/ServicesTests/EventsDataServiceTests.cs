using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSJennings.EFCoreDemo.Business.DataAccess;
using MSJennings.EFCoreDemo.Business.Models;
using MSJennings.EFCoreDemo.Business.Services;

namespace MSJennings.EFCoreDemo.Tests.ServicesTests
{
    [TestClass]
    public class EventsDataServiceTests
    {
        [TestMethod]
        public void GetEvent_WithValidId_ShouldReturnExpectedResult()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(nameof(GetEvent_WithValidId_ShouldReturnExpectedResult))
                .Options;

            var dbContext = new AppDbContext(dbContextOptions);

            dbContext.Events.AddRange(
                new Event { Id = 101, Title = "Event One" },
                new Event { Id = 102, Title = "Event Two" },
                new Event { Id = 103, Title = "Event Three" });

            dbContext.SaveChanges();

            var eventsDataService = new EventsDataService(dbContext);

            // Act
            var result = eventsDataService.GetEvent(102);

            // Assert
            Assert.IsNotNull(result, "Result is null");
            Assert.AreEqual(102, result.Id, $"Result {nameof(Event.Id)} is not the expected value");
            Assert.AreEqual("Event Two", result.Title, $"Result {nameof(Event.Title)} is not the expected value");
        }
    }
}
