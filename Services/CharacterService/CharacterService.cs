using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.dtos;
using dotnet_rpg.dtos.Character;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character{Id = 1, Name = "Demo"}
        };

        private IMapper _mapper;
        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceRespose<List<GetCharacterDto>>> addCharacter(AddCharacterDto character)
        {
            var mainCharacter = _mapper.Map<Character>(character);
            mainCharacter.Id = characters.Max(x => x.Id)+1;
            characters.Add(mainCharacter);
            var characterDto = characters.Select(x => _mapper.Map<GetCharacterDto>(x)).ToList();
            ServiceRespose<List<GetCharacterDto>> serviceRespose = new ServiceRespose<List<GetCharacterDto>>{Data = characterDto};
            return serviceRespose;
        }

        public async Task<ServiceRespose<List<GetCharacterDto>>> getAll()
        {
             var characterDto = characters.Select(x => _mapper.Map<GetCharacterDto>(x)).ToList();
            ServiceRespose<List<GetCharacterDto>> serviceRespose = new ServiceRespose<List<GetCharacterDto>>{Data = characterDto};
            return serviceRespose;
        }

        public async Task<ServiceRespose<GetCharacterDto>> getSingle(int id)
        {
            var character = characters.FirstOrDefault(x => x.Id == id);
            ServiceRespose<GetCharacterDto> serviceRespose = new ServiceRespose<GetCharacterDto>{Data = _mapper.Map<GetCharacterDto>(character)};
            return serviceRespose;
        }

        public async Task<ServiceRespose<GetCharacterDto>> updateCharacter(UpdateCharacterDto character, int id)
        {
            var existCharacter = characters.FirstOrDefault(x => x.Id == id);
            if(existCharacter is null){
                return new ServiceRespose<GetCharacterDto>{Data = null, Success = false, Message = $"CHaracter doesnot exist with id '{id}'"};
            }
            _mapper.Map(character, existCharacter);
            return new ServiceRespose<GetCharacterDto>{Data = _mapper.Map<GetCharacterDto>(character)};
        }
    }
}