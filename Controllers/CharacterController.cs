using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.dtos;
using dotnet_rpg.dtos.Character;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController: ControllerBase
    {
        private ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceRespose<List<GetCharacterDto>>>> GetAll(){
            return Ok(await _characterService.getAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRespose<GetCharacterDto>>> GetSingle(int id){
            return Ok(await _characterService.getSingle(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceRespose<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto character){
            return Ok(await _characterService.addCharacter(character));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceRespose<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto character, int id){
            var updatedCHaracter = await _characterService.updateCharacter(character, id);
            if(updatedCHaracter.Success is false)
                return NotFound(updatedCHaracter);
            return Ok(updatedCHaracter);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceRespose<List<GetCharacterDto>>>> DeleteCharacter(int id){
            var deletedResponse = await _characterService.deleteCharacter(id);
            if(deletedResponse.Success is false)
                return NotFound(deletedResponse);
            return Ok(deletedResponse);
        }

    }
}