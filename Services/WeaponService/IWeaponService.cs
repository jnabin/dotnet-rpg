using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.dtos;
using dotnet_rpg.dtos.Weapon;

namespace dotnet_rpg.Services.WeaponService
{
    public interface IWeaponService
    {
        public Task<ServiceRespose<GetCharacterDto>> AddWeapon(AddWeaponDto addWeaponDto);
        
    }
}