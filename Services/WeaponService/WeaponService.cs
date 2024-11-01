using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.dtos;
using dotnet_rpg.dtos.Character;
using dotnet_rpg.dtos.Weapon;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private DataContext _context;
        private IMapper _mapper;
        private IHttpContextAccessor _httpContext;
        public WeaponService(DataContext dataContext, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _context = dataContext;
            _mapper = mapper;
            _httpContext = httpContext;
        }
        public async Task<ServiceRespose<GetCharacterDto>> AddWeapon(AddWeaponDto addWeaponDto)
        {
            var response = new ServiceRespose<GetCharacterDto>();
            try{
                var character = await _context.Characters
                    .FirstOrDefaultAsync(x => x.Id == addWeaponDto.CharacterId && 
                        x.User!.Id == int.Parse(_httpContext.HttpContext!.User
                            .FindFirstValue(ClaimTypes.NameIdentifier)!));
                if(character is null){
                    response.Success = false;
                    response.Message = $"Character not found with id {addWeaponDto.CharacterId}";
                    return response;
                }

                var weapon = new Weapon{
                    Name = addWeaponDto.Name,
                    Damage = addWeaponDto.Damage,
                    Character = character
                };

                _context.Weapons.Add(weapon);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);
                return response;
            } catch (Exception exc) {
                response.Success = false;
                response.Message = exc.Message;
                return response;
            }
        }
    }
}