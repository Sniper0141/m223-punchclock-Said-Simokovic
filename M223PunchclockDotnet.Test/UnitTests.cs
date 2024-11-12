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
        dbContextMock?.Dispose();
    }

    [Test]
    public void Test1()
    {
        var service = new CategoryService(dbContextMock);
    }

    [Test]
    public void Test2()
    {
        var service = new CategoryService(dbContextMock);
    }
}