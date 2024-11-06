using M223PunchclockDotnet.Model;
using M223PunchclockDotnet.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using M223PunchclockDotnet.DataTransfer;

namespace M223PunchclockDotnet.Controllers;

[ApiController]
[Route("category")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService categoryService;
    private readonly TestDataRepository testDataRepository;

    public CategoryController(CategoryService categoryService, TestDataRepository testDataRepository)
    {
        this.categoryService = categoryService;
        this.testDataRepository = testDataRepository;
    }

    [HttpGet]
    [ProducesResponseType<Category>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var allCategories = await categoryService.FindAll();
        allCategories.AddRange(testDataRepository.TestCategories);
        return Ok(allCategories);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<Category>(StatusCodes.Status201Created)]
    public async Task<ActionResult<Category>> AddCategory(NewCategoryData categoryData){
        var newElement = await categoryService.AddCategory(categoryData);

        return CreatedAtAction(nameof(Get), new{id = categoryData.Id}, categoryData);
    }

    [HttpDelete]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> DeleteCategoryWithId(int id)
    {
        await categoryService.DeleteCategory(id);
        return NoContent();
    }

    [HttpPatch("{id:int}")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> EditCategoryWithId(int id, EditedCategoryData editedCategoryData)
    {
        await categoryService.EditCategory(id, editedCategoryData);
        return Ok();
    }
}