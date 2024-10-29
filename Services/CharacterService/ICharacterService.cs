using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.dtos;
using dotnet_rpg.dtos.Character;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceRespose<List<GetCharacterDto>>> getAll();
        Task<ServiceRespose<GetCharacterDto>> getSingle(int id);
        Task<ServiceRespose<List<GetCharacterDto>>> addCharacter(AddCharacterDto character);
    }
}