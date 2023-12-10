using System;
using System.Collections.Generic;

namespace LoreLayer
{
    public class SkillNode
    {
        // Existing properties...

        public List<SkillNode> Prerequisites { get; private set; }
        public Action<Character> SkillEffect { get; private set; }

        public SkillNode(string name, string description, int levelRequirement, Action<Character> skillEffect)
        {
            // Existing initializations...
            Prerequisites = new List<SkillNode>();
            SkillEffect = skillEffect;
        }

        public void AddPrerequisite(SkillNode prerequisite)
        {
            Prerequisites.Add(prerequisite);
        }

        public bool CanUnlock(Character character)
        {
            // Check if prerequisites are met
            return true; // Placeholder
        }

        // Additional methods...
    }
}
