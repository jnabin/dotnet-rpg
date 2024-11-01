using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.dtos.Skill;
using dotnet_rpg.dtos.Weapon;

namespace dotnet_rpg.dtos
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 20;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Mage;
        public GetWeaponDto? Weapon { get; set; }
        public List<GetSkillDto>? Skills { get; set; }
    }
}