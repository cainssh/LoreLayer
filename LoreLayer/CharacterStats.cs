using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoreLayer.Characters
{
    public class CharacterStats
    {
        public int Strength { get; set; }
        public int Intelligence { get; set; }

        public int Experience { get; private set; }

        public void AddExperience(int xp)
        {
            Experience += xp;
        }
        public CharacterStats()
        {
            // Initialize default stats
        }

        public void ApplyModifiers(StatModifier modifier)
        {
            // Apply stat modifications
        }

        // Additional methods related to character stats
    }
}

