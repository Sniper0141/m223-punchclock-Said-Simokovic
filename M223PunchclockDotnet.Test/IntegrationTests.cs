using M223PunchclockDotnet.DataTransfer;
using M223PunchclockDotnet.Model;
using M223PunchclockDotnet.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace M223PunchclockDotnet.Test;

public class IntegrationTests
{
    private ServiceProvider serviceProvider;
    
    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();

        // Using In-Memory database for testing
        services.AddDbContext<DatabaseContext>(options =>
            options.UseInMemoryDatabase("TestDb"));
        services.AddScoped<EntryService, EntryService>();
        services.AddScoped<TestDataRepository, TestDataRepository>();

        serviceProvider = services.BuildServiceProvider();
    }

    [TearDown]
    public async Task TearDown()
    {
        await serviceProvider.DisposeAsync();
    }

    [Test]
    public async Task Test()
    {
        // Arrange 
        await using var scope = serviceProvider.CreateAsyncScope();
        var entryService = scope.ServiceProvider.GetService<EntryService>();
        var context = scope.ServiceProvider.GetService<DatabaseContext>();

        var entryData = new NewEntryData { CategoryId = 1, CheckIn = DateTime.Parse("08/18/2018 07:22:16") };
            
        // Act
        await entryService!.AddEntry(entryData);
        
        // Assert
        var actual =
            await context!.Entries.SingleOrDefaultAsync(e => e.CheckIn == DateTime.Parse("08/18/2018 07:22:16"));
        Assert.That(actual is not null);
        Console.WriteLine(actual);
    }
}