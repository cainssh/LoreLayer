using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoreLayer.Characters;
using LoreLayer.Combat;

namespace LoreLayer
{
    public class PluginMain
    {
        public InventoryManager Inventory { get; private set; }
        public SkillTree SkillTree { get; private set; }
        public Character Character { get; private set; }
        public CombatSystem CombatSystem { get; private set; }

        public PluginMain(int inventoryCapacity)
        {
            Inventory = new InventoryManager(inventoryCapacity);
            SkillTree = new SkillTree();
            Character = new Character("Default Character");
            CombatSystem = new CombatSystem(Character);
        }

        // Additional initialization and management methods
    }
}

