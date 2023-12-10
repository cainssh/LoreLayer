using System;
using System.Collections.Generic;
using LoreLayer;

namespace LoreLayer.Characters
{
    public class Character
    {
        public string Name { get; private set; }
        public CharacterStats Stats { get; private set; }
        public InventoryManager Inventory { get; private set; }
        public SkillTree SkillTree { get; private set; }
        public int Level { get; private set; }
        public int ExperiencePoints { get; private set; }

        // Constructors, properties, and methods...

        public Character(string name)
        {
            Name = name;
            Stats = new CharacterStats();
            Inventory = new InventoryManager(20);
            SkillTree = new SkillTree();
            Level = 1;
            ExperiencePoints = 0;
        }

        public void GainExperience(int amount)
        {
            ExperiencePoints += amount;
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            // Logic for leveling up
        }

        public void UnlockSkill(SkillNode skillNode)
        {
            if (Level >= skillNode.LevelRequirement)
            {
                SkillTree.UnlockSkillNode(skillNode);
            }
        }

        // Additional character functionalities, like interacting with quests, items, etc.
        // ...
    }
}
