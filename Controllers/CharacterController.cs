using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dotnet_rpg.dtos;
using dotnet_rpg.dtos.Character;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [Authorize(Roles = "Player,Admin")]
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
            //int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            return Ok(await _characterService.getAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRespose<GetCharacterDto>>> GetSingle(int id){
            return Ok(await _characterService.getSingle(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceRespose<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto character){
            var response = await _characterService.addCharacter(character);
            return Created(Request.Path.ToString()+$"/{response.Data!.LastOrDefault()!.Id}", response);
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

        [HttpPost("addSkill")]
        public async Task<ActionResult<ServiceRespose<GetCharacterDto>>> AddSkill(AddSkillToCharacterDto request){
            var response = await _characterService.AddSkillToCharacter(request);
            if(!response.Success){
                return BadRequest(response);
            } 

            return Ok(response);
        }

    }
}