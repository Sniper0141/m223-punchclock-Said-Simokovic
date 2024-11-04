using System.Net.Mime;
using M223PunchclockDotnet.Model;
using M223PunchclockDotnet.Service;
using Microsoft.AspNetCore.Mvc;

namespace M223PunchclockDotnet.Controllers
{
    [ApiController]
    [Route("entry")]
    public class EntryController : ControllerBase
    {
        private readonly EntryService entryService;

        public EntryController(EntryService entryService)
        {
            this.entryService = entryService;
        }

        [HttpGet]
        [ProducesResponseType<Entry>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var allEntries = await entryService.FindAll();
            return Ok(allEntries);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType<Entry>(StatusCodes.Status201Created)]
        public async Task<ActionResult<Entry>> AddEntry(Entry entry){
            var newElement = await entryService.AddEntry(entry);

            return CreatedAtAction(nameof(Get), new{id = entry.Id}, entry);
        }

        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteEntryWithId(int id)
        {
            await entryService.DeleteEntry(id);
            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> EditEntryWithId(int id, EditedEntryData editedEntryData)
        {
            await entryService.EditEntry(id, editedEntryData);
            return Ok();
        }
    }
}
