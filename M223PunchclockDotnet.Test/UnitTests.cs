using M223PunchclockDotnet.Model;
using M223PunchclockDotnet.Service;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace M223PunchclockDotnet.Test;

public class UnitTests
{
    private DatabaseContext dbContextMock;
    
    [SetUp]
    public void Setup()
    {
        var optionsMock = new DbContextOptions<DatabaseContext>();
        var mock = new Mock<DatabaseContext>(optionsMock);

        CreateTestData(mock);
        
        dbContextMock = mock.Object;
    }

    private static void CreateTestData(Mock<DatabaseContext> mock)
    {
        var category = new Category
        {
            Id = 1,
            Title = "Basic-Category"
        };
        var entry = new Entry
        {
            Id = 1,
            CheckIn = DateTime.MinValue,
            CheckOut = DateTime.MaxValue,
            CategoryId = 1,
            Category = category
        };
        var tag = new Tag
        {
            Id = 1,
            Title = "Basic-Tag"
        };
        
        mock.Setup(c => c.Entries).ReturnsDbSet(new List<Entry> { entry });
        mock.Setup(c => c.Categories).ReturnsDbSet(new List<Category> { category });
        mock.Setup(c => c.Tags).ReturnsDbSet(new List<Tag> { tag });
    }

    [TearDown]
    public void TearDown()
    {
        dbContextMock.Dispose();
    }

    [Test]
    public async Task FindAll_Entries_ReturnsCorrectly()
    {
        // Arrange
        var service = new EntryService(dbContextMock);
        
        // Act
        var entries = await service.FindAll();
        
        // Assert
        Assert.That(entries != null);
    }

    [Test]
    public async Task FindAll_Categories_ReturnsCorrectly()
    {
        // Arrange
        var service = new CategoryService(dbContextMock);
        
        // Act
        var categories = await service.FindAll();
        
        // Assert
        Assert.That(categories != null);
    }

    [Test]
    public async Task FindAll_Tags_ReturnsCorrectly()
    {
        // Arrange
        var service = new TagService(dbContextMock);
        
        // Act
        var tags = await service.FindAll();
        
        // Assert
        Assert.That(tags != null);
    }
}