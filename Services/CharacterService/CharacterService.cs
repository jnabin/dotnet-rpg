using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character{Id = 1, Name = "Demo"}
        };
        public async Task<ServiceRespose<List<Character>>> addCharacter(Character character)
        {
            characters.Add(character);
            ServiceRespose<List<Character>> serviceRespose = new ServiceRespose<List<Character>>{Data = characters};
            return serviceRespose;
        }

        public async Task<ServiceRespose<List<Character>>> getAll()
        {
            ServiceRespose<List<Character>> serviceRespose = new ServiceRespose<List<Character>>{Data = characters};
            return serviceRespose;
        }

        public async Task<ServiceRespose<Character>> getSingle(int id)
        {
            var character = characters.FirstOrDefault(x => x.Id == id);
            ServiceRespose<Character> serviceRespose = new ServiceRespose<Character>{Data = character};
            return serviceRespose;
        }
    }
}