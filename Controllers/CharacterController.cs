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
        public ActionResult<List<Character>> getAll(){
            return Ok(_characterService.getAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Character> GetSingle(int id){
            return Ok(_characterService.getSingle(id));
        }

        [HttpPost]
        public ActionResult<List<Character>> AddCharacter(Character character){
            return Ok(_characterService.addCharacter(character));
        }
    }
}