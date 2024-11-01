using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.dtos.Character
{
    public class AddSkillToCharacterDto
    {
        public int CharacterId { get; set; }
        public int SkillId { get; set; }
    }
}