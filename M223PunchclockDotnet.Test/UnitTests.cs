using M223PunchclockDotnet.DataTransfer;
using M223PunchclockDotnet.Model;
using M223PunchclockDotnet.Service;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace M223PunchclockDotnet.Test;

public class Tests
{
    private DatabaseContext dbContextMock;
    
    [SetUp]
    public void Setup()
    {
        var optionsMock = new DbContextOptions<DatabaseContext>();
        var contextMock = new Mock<DatabaseContext>(optionsMock);
        contextMock.Setup(c => c.Categories).ReturnsDbSet(new List<Category>());
        dbContextMock = contextMock.Object;
    }

    [TearDown]
    public void TearDown()
    {
        dbContextMock.Dispose();
    }

    [Test]
    public async Task AddCategory_WithValidData_ShouldPersist()
    {
        // Arrange
        var service = new CategoryService(dbContextMock);
        var categoryData = new NewCategoryData
        {
            Title = "Test-Category"
        };
        
        // Act
        await service.AddCategory(categoryData);
        
        // Assert
        var categories = await service.FindAll();
        Assert.That(categories.Count == 1);
        Assert.That(categories[0].Title == categoryData.Title);
    }

    [Test]
    public async Task EditCategory_WithNewTitle_TitleShouldChange()
    {
        // Arrange
        var service = new CategoryService(dbContextMock);
        var categoryData = new NewCategoryData
        {
            Title = "Test-Category"
        };
        await service.AddCategory(categoryData);
        
        // Act
        var editedTitle = "edited-title";
        await service.EditCategory(1, new EditedCategoryData { Title = editedTitle });
        
        // Assert
        var categories = await service.FindAll();
        Assert.That(categories.Count == 1);
        Assert.That(categories[0].Title == editedTitle);
    }

    [Test]
    public async Task DeleteCategory_ShouldDeletePersistedCategory()
    {
        // Arrange
        var service = new CategoryService(dbContextMock);
        var categoryData = new NewCategoryData
        {
            Title = "Test-Category"
        };
        await service.AddCategory(categoryData);
        
        // Act
        await service.DeleteCategory(1);
        
        // Assert
        var categories = await service.FindAll();
        Assert.That(categories.Count == 0);
    }
}