using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.dtos;
using dotnet_rpg.dtos.Character;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character{Id = 1, Name = "Demo"}
        };
        private DataContext _context;
        private IHttpContextAccessor _httpContextAccessor;
        private IMapper _mapper;
        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = contextAccessor;
        }

        public int getUserId() => int.Parse(_httpContextAccessor.HttpContext!
            .User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceRespose<List<GetCharacterDto>>> addCharacter(AddCharacterDto character)
        {
            var mainCharacter = _mapper.Map<Character>(character);
            mainCharacter.User = await _context.Users.FirstOrDefaultAsync(x => x.Id == getUserId());
            _context.Characters.Add(mainCharacter);
            await _context.SaveChangesAsync();
            var characterDto = await _context.Characters
                .Where(x => x.User!.Id == getUserId())
                .Select(x => _mapper.Map<GetCharacterDto>(x))
                .ToListAsync();
            ServiceRespose<List<GetCharacterDto>> serviceRespose = new ServiceRespose<List<GetCharacterDto>>{Data = characterDto};
            return serviceRespose;
        }

        public async Task<ServiceRespose<List<GetCharacterDto>>> getAll()
        {
            var dbCharacters = await _context.Characters.Where(x => x.User!.Id == getUserId()).ToListAsync();
            var characterDto =  dbCharacters.Select(x => _mapper.Map<GetCharacterDto>(x)).ToList();
            ServiceRespose<List<GetCharacterDto>> serviceRespose = new ServiceRespose<List<GetCharacterDto>>{Data = characterDto};
            return serviceRespose;
        }

        public async Task<ServiceRespose<GetCharacterDto>> getSingle(int id)
        {
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);
            ServiceRespose<GetCharacterDto> serviceRespose = new ServiceRespose<GetCharacterDto>{Data = _mapper.Map<GetCharacterDto>(character)};
            return serviceRespose;
        }

        public async Task<ServiceRespose<GetCharacterDto>> updateCharacter(UpdateCharacterDto character, int id)
        {
            var existCharacter = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);
            if(existCharacter is null){
                return new ServiceRespose<GetCharacterDto>{Data = null, Success = false, Message = $"CHaracter doesnot exist with id '{id}'"};
            }
            _mapper.Map(character, existCharacter);
            return new ServiceRespose<GetCharacterDto>{Data = _mapper.Map<GetCharacterDto>(character)};
        }

        public async Task<ServiceRespose<List<GetCharacterDto>>> deleteCharacter(int id)
        {
            var response = new ServiceRespose<List<GetCharacterDto>>();
            try{
                var existCharacter = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);
                if(existCharacter is null){
                    throw new Exception($"Character doesnot exist with id '{id}'");
                }
                characters.Remove(existCharacter);
                response.Data = characters.Select(x => _mapper.Map<GetCharacterDto>(x)).ToList();
            } catch(Exception exc){
                response.Success = false;
                response.Message = exc.Message;
            }
            return response;
        }
    }
}