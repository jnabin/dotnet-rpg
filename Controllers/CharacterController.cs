using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult<ServiceRespose<List<Character>>>> GetAll(){
            return Ok(await _characterService.getAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRespose<Character>>> GetSingle(int id){
            return Ok(await _characterService.getSingle(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceRespose<List<Character>>>> AddCharacter(Character character){
            return Ok(await _characterService.addCharacter(character));
        }
    }
}