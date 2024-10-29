using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceRespose<List<Character>>> getAll();
        Task<ServiceRespose<Character>> getSingle(int id);
        Task<ServiceRespose<List<Character>>> addCharacter(Character character);
    }
}