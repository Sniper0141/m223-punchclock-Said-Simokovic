using M223PunchclockDotnet.DataTransfer;
using M223PunchclockDotnet.Model;
using Microsoft.EntityFrameworkCore;

namespace M223PunchclockDotnet.Service;

public class TagService(DatabaseContext databaseContext)
{
    public Task<List<Tag>> FindAll()
    {
        return databaseContext.Tags.ToListAsync();
    }

    public async Task<Tag> AddTag(NewTagData tagData)
    {
        var tag = new Tag
        {
            Title = tagData.Title
        };
        databaseContext.Categories.Add(tag);
        await databaseContext.SaveChangesAsync();

        return tag;
    }

    public async Task DeleteTag(int tagId)
    {
        var tag = await databaseContext.FindAsync<Tag>(tagId);
        if (tag is null)
        {
            throw new NullReferenceException($"No tag with id {tagId} found.");
        }
            
        databaseContext.Categories.Remove(tag);
        await databaseContext.SaveChangesAsync();
    }

    public async Task EditTag(int tagId, EditedTagData tagData)
    {
        var tag = await databaseContext.FindAsync<Tag>(tagId);
        if (tag is null)
        {
            throw new NullReferenceException($"No tag with id {tagId} found.");
        }

        if (tagData.Title is null)
        {
            return;
        }
        
        tag.Title = tagData.Title;
    }
}