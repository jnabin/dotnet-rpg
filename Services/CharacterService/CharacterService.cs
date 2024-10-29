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
        public List<Character> addCharacter(Character character)
        {
            characters.Add(character);
            return characters;
        }

        public List<Character> getAll()
        {
            return characters;
        }

        public Character getSingle(int id)
        {
            return characters.FirstOrDefault(x => x.Id == id);
        }
    }
}