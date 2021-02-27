using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        public CommandsController(ICommanderRepo repository , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var command = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(command));
        }

        [HttpGet("{Id}", Name = "GetCommandById")]
        public ActionResult <CommandReadDto> GetCommandById(int Id)
        {
            var command = _repository.GetCommandById(Id);
            if(command != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(command));
            }
            
            return NotFound();
        }

        [HttpPost]
        public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var command = _mapper.Map<Command>(commandCreateDto);
            _repository.createCommand(command);
            _repository.SaveChanges();
            var commandReadDto = (_mapper.Map<CommandReadDto>(command));
            return CreatedAtRoute(nameof(GetCommandById), new {commandReadDto.Id} , commandReadDto );
        }

        [HttpPut("{Id}")]
        public ActionResult UpdateCommandById(int id , CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(commandUpdateDto, commandModelFromRepo );
            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id , JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }
            var commandToPatch =  _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch,ModelState);
            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(commandToPatch,commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}