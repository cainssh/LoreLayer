using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoreLayer.Characters
{
    public class StatModifier
    {
        public enum ModifierType { Flat, Percent }

        public int StrengthModifier { get; private set; }
        public int IntelligenceModifier { get; private set; }

        private ModifierType type;

        public StatModifier(int strength, int intelligence, ModifierType type)
        {
            StrengthModifier = strength;
            IntelligenceModifier = intelligence;
            this.type = type;
        }

        public void Apply(CharacterStats stats)
        {
            switch (type)
            {
                case ModifierType.Flat:
                    stats.Strength += StrengthModifier;
                    stats.Intelligence += IntelligenceModifier;
                    break;
                case ModifierType.Percent:
                    stats.Strength += (stats.Strength * StrengthModifier) / 100;
                    stats.Intelligence += (stats.Intelligence * IntelligenceModifier) / 100;
                    break;
            }
        }

        public void Remove(CharacterStats stats)
        {
            // Logic to remove this modifier from stats
            // Similar to Apply, but in reverse
        }

        // Additional methods or properties for stat modification
    }
}

