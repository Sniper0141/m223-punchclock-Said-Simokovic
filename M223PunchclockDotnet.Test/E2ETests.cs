using M223PunchclockDotnet.Controllers;
using M223PunchclockDotnet.DataTransfer;
using M223PunchclockDotnet.Model;
using M223PunchclockDotnet.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace M223PunchclockDotnet.Test;

public class E2ETests
{
    private EntryService entryService;
    private TestDataRepository testDataRepository;
    private DatabaseContext context;
    
    [SetUp]
    public void SetUp()
    {
        var services = new ServiceCollection();

        // Using In-Memory database for testing
        services.AddDbContext<DatabaseContext>(options =>
            options.UseInMemoryDatabase("TestDb"));
        var serviceProvider = services.BuildServiceProvider();

        entryService = new EntryService(serviceProvider.GetService<DatabaseContext>()!);
        testDataRepository = new TestDataRepository();
        context = serviceProvider.GetService<DatabaseContext>()!;
    }

    [Test]
    public async Task Test()
    {
        // Arrange
        var controller = new EntryController(entryService, testDataRepository);
        
        // Act
        var entryData = new NewEntryData
        {
            CategoryId = 1,
            CheckIn = DateTime.MinValue,
            CheckOut = DateTime.MinValue
        };
        await controller.AddEntry(entryData);
        
        // Assert
        var actual = await context.Entries.SingleOrDefaultAsync(e => e.CheckIn == DateTime.MinValue);
        Assert.That(actual is not null);
    }
}