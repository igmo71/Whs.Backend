using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Whs.Application.Services.Notes.Commands.CreateNote;
using Whs.Application.Services.Notes.Commands.DeleteNote;
using Whs.Application.Services.Notes.Commands.UpdateNote;
using Whs.Application.Services.Notes.Queries.GetNoteDetails;
using Whs.Application.Services.Notes.Queries.GetNoteList;
using Whs.WebApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace Whs.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class NotesController : BaseController
    {
        private IMapper _mapper;
        
        public NotesController(IMapper mapper) => _mapper = mapper;

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery { UserId = UserId };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
        {
            var query = new GetNoteDetailsQuery { Id = id, UserId = UserId };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
        {
            var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
            command.UserId = UserId;
            var id = await Mediator.Send(command);
            return CreatedAtAction("Get", id);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
        {
            var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteNoteCommand { Id= id, UserId = UserId };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
