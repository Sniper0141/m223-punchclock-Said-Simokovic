using System.Net.Mime;
using M223PunchclockDotnet.DataTransfer;
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
        private readonly TestDataRepository testDataRepository;

        public EntryController(EntryService entryService, TestDataRepository testDataRepository)
        {
            this.entryService = entryService;
            this.testDataRepository = testDataRepository;
        }

        [HttpGet]
        [ProducesResponseType<Entry>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var allEntries = await entryService.FindAll();
            allEntries.AddRange(testDataRepository.TestEntries);
            return Ok(allEntries);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType<Entry>(StatusCodes.Status201Created)]
        public async Task<ActionResult<Entry>> AddEntry(NewEntryData entryData){
            var newElement = await entryService.AddEntry(entryData);

            return CreatedAtAction(nameof(Get), new{id = entryData.Id}, entryData);
        }

        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteEntryWithId(int id)
        {
            await entryService.DeleteEntry(id);
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> EditEntryWithId(int id, EditedEntryData editedEntryData)
        {
            await entryService.EditEntry(id, editedEntryData);
            return Ok();
        }
    }
}
