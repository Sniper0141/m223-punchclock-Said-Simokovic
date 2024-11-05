using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using M223PunchclockDotnet.Model;
using M223PunchclockDotnet.Service;
using M223PunchclockDotnet.DataTransfer;

namespace M223PunchclockDotnet.Controllers;

[ApiController]
[Route("tag")]
public class TagController : ControllerBase
{
    private readonly TagService tagService;

    public TagController(TagService tagService)
    {
        this.tagService = tagService;
    }

    [HttpGet]
    [ProducesResponseType<Tag>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var allEntries = await tagService.FindAll();
        return Ok(allEntries);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<Tag>(StatusCodes.Status201Created)]
    public async Task<ActionResult<Tag>> AddTag(NewTagData tagData){
        var newElement = await tagService.AddTag(tagData);

        return CreatedAtAction(nameof(Get), new{id = tagData.Id}, tagData);
    }

    [HttpDelete]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> DeleteTagWithId(int id)
    {
        await tagService.DeleteTag(id);
        return NoContent();
    }

    [HttpPatch("{id:int}")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> EditTagWithId(int id, EditedTagData editedTagData)
    {
        await tagService.EditTag(id, editedTagData);
        return Ok();
    }
}