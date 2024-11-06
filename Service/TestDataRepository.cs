using M223PunchclockDotnet.Model;

namespace M223PunchclockDotnet.Service;

public class TestDataRepository
{
    public List<Category> TestCategories { get; } = [
        new()
        {
            Id = 1,
            Title = "category 1"
        },
        new()
        {
            Id = 2,
            Title = "category 2"
        },
        new()
        {
            Id = 3,
            Title = "category 3"
        }
    ];
    
    public List<Entry> TestEntries { get; } =
    [
        new()
        {
            Id = 1,
            CheckIn = DateTime.Now.AddDays(-2),
            CheckOut = DateTime.Now.AddDays(-2),
            CategoryId = 1
        },
        new()
        {
            Id = 2,
            CheckIn = DateTime.Now.AddDays(-1),
            CheckOut = DateTime.Now.AddDays(-1),
            CategoryId = 2
        },
        new()
        {
            Id = 3,
            CheckIn = DateTime.Now,
            CheckOut = DateTime.Now,
            CategoryId = 3
        },
    ];

    public List<Tag> TestTags { get; } = 
    [
        new()
        {
            Id = 1,
            Title = "tag 1"
        },
        new()
        {
            Id = 2,
            Title = "tag 2"
        },
        new()
        {
            Id = 3,
            Title = "tag 3"
        }
    ];
    
    public TestDataRepository()
    {
        TestCategories[0].Entries.Add(TestEntries[0]);
        TestCategories[1].Entries.Add(TestEntries[1]);
        TestCategories[2].Entries.Add(TestEntries[2]);

        TestEntries[0].Category = TestCategories[0];
        TestEntries[0].Tags.Add(TestTags[0]);
        TestEntries[1].Category = TestCategories[1];
        TestEntries[1].Tags.Add(TestTags[1]);
        TestEntries[2].Category = TestCategories[2];
        TestEntries[2].Tags.Add(TestTags[2]);
        
        TestTags[0].Entries.Add(TestEntries[0]);
        TestTags[1].Entries.Add(TestEntries[1]);
        TestTags[2].Entries.Add(TestEntries[2]);
    }
}