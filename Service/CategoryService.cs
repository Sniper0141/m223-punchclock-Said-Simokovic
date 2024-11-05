using M223PunchclockDotnet.DataTransfer;
using M223PunchclockDotnet.Model;
using Microsoft.EntityFrameworkCore;

namespace M223PunchclockDotnet.Service;

public class CategoryService(DatabaseContext databaseContext)
{
    public Task<List<Category>> FindAll()
    {
        return databaseContext.Categories.ToListAsync();
    }

    public async Task<Category> AddCategory(NewCategoryData categoryData)
    {
        var category = new Category
        {
            Title = categoryData.Title
        };
        databaseContext.Categories.Add(category);
        await databaseContext.SaveChangesAsync();

        return category;
    }

    public async Task DeleteCategory(int categoryId)
    {
        var category = await databaseContext.FindAsync<Category>(categoryId);
        if (category is null)
        {
            throw new NullReferenceException($"No category with id {categoryId} found.");
        }
            
        databaseContext.Categories.Remove(category);
        await databaseContext.SaveChangesAsync();
    }

    public async Task EditCategory(int categoryId, EditedCategoryData categoryData)
    {
        var category = await databaseContext.FindAsync<Category>(categoryId);
        if (category is null)
        {
            throw new NullReferenceException($"No category with id {categoryId} found.");
        }

        if (categoryData.Title is null)
        {
            return;
        }
        
        category.Title = categoryData.Title;
    }
}