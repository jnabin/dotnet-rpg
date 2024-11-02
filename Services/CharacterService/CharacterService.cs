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

        public string getUserRole() => _httpContextAccessor.HttpContext!
            .User.FindFirstValue(ClaimTypes.Role)!;

        public async Task<ServiceRespose<List<GetCharacterDto>>> addCharacter(AddCharacterDto character)
        {
            var mainCharacter = _mapper.Map<Character>(character);
            mainCharacter.User = await _context.Users.FirstOrDefaultAsync(x => x.Id == getUserId());

            _context.Characters.Add(mainCharacter);
            await _context.SaveChangesAsync();
                            var dbCharacters = await getAllCharacterObject();
            var characterDto = dbCharacters.Select(x => _mapper.Map<GetCharacterDto>(x)).ToList();
            
            ServiceRespose<List<GetCharacterDto>> serviceRespose = new ServiceRespose<List<GetCharacterDto>>{Data = characterDto};

            return serviceRespose;
        }

        public async Task<ServiceRespose<List<GetCharacterDto>>> getAll()
        {
            var dbCharacters = await getAllCharacterObject();
            var characterDto =  dbCharacters.Select(x => _mapper.Map<GetCharacterDto>(x)).ToList();
            ServiceRespose<List<GetCharacterDto>> serviceRespose = 
                new ServiceRespose<List<GetCharacterDto>>{Data = characterDto};

            return serviceRespose;
        }

        public async Task<ServiceRespose<GetCharacterDto>> getSingle(int id)
        {
            var character = await getCharacterObject(id);
            ServiceRespose<GetCharacterDto> serviceRespose = 
                new ServiceRespose<GetCharacterDto>{Data = _mapper.Map<GetCharacterDto>(character)};
            return serviceRespose;
        }

        public async Task<ServiceRespose<GetCharacterDto>> updateCharacter(UpdateCharacterDto character, int id)
        {
            var existCharacter = await getCharacterObject(id);
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
                var existCharacter = await getCharacterObject(id);
                if(existCharacter is null){
                    throw new Exception($"Character doesnot exist with id '{id}'");
                }
                _context.Characters.Remove(existCharacter);
                await _context.SaveChangesAsync();
                var dbCharacters = await getAllCharacterObject();
                response.Data = dbCharacters.Select(x => _mapper.Map<GetCharacterDto>(x)).ToList();
            } catch(Exception exc){
                response.Success = false;
                response.Message = exc.Message;
            }
            return response;
        }

        private async Task<Character?> getCharacterObject(int id){
            Character? character = await _context.Characters!.Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(x => x.Id == id && x.User!.Id == getUserId());
            return character;
        }

        private async Task<List<Character>> getAllCharacterObject(){
            var character = new List<Character>();
            if(getUserRole() == "Admin"){
                character = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .ToListAsync();
            } else {
                character = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .Where(x => x.User!.Id == getUserId())
                    .ToListAsync();
            }
            return character;
        }

        public async Task<ServiceRespose<GetCharacterDto>> AddSkillToCharacter(AddSkillToCharacterDto addSkillToCharacterDto)
        {
            var response = new ServiceRespose<GetCharacterDto>();
            try
            {
                var character = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(x => x.Id == addSkillToCharacterDto.CharacterId 
                        && x.User!.Id == getUserId());
                if(character is null){
                    response.Success = false;
                    response.Message = $"Character not found with characterId - {addSkillToCharacterDto.CharacterId}";
                    return response;
                }

                if(character.Skills!.Select(x => x.Id).Contains(addSkillToCharacterDto.SkillId)){
                    response.Success = false;
                    response.Message = $"Skill already added into the Character";
                    return response;
                }

                var skill = await _context.Skills.FirstOrDefaultAsync(x => x.Id == addSkillToCharacterDto.SkillId);
                if(skill is null){
                    response.Success = false;
                    response.Message = $"CharSkillacter not found with skillId - {addSkillToCharacterDto.SkillId}";
                    return response;
                }

                character.Skills!.Add(skill);
                await _context.SaveChangesAsync();
                
                response.Data = _mapper.Map<GetCharacterDto>(character);
                return response;
            }
            catch (Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
                return response;
            }
        }
    }
}