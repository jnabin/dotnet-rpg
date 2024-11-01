using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.dtos;
using dotnet_rpg.dtos.Character;
using dotnet_rpg.dtos.Skill;
using dotnet_rpg.dtos.Weapon;

namespace dotnet_rpg.profiles
{
    public class CharacterMapProfile : Profile
    {
        public CharacterMapProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, GetCharacterDto>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
        }   
    }
}