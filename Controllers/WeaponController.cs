using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.dtos;
using dotnet_rpg.dtos.Weapon;
using dotnet_rpg.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[COntroller]")]
    public class WeaponController: ControllerBase
    {
        private IWeaponService _weaponService;
        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }   

        [HttpPost]
        public async Task<ActionResult<GetCharacterDto>> AddWeapon(AddWeaponDto addWeaponDto){
            var response = await _weaponService.AddWeapon(addWeaponDto);
            if(response.Success){
                return Created($"{Request.Path.ToString()}/{response.Data!.Id}", response);
            } else {
                return BadRequest(response);
            }
        }
    }
}