using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoreLayer
{
    public class PerishableItem : Item
    {

        public PerishableItem(string id, string name, string description, int quantity, int weight, int value, ItemType type, ItemRarity rarity, bool isTradable, bool isConsumable, DateTime expirationDate)
            : base(id, name, description, quantity, weight, value, type, rarity, isTradable, isConsumable)
        {
            ExpirationDate = expirationDate;
        }

        public new bool IsExpired() // 'new' keyword to explicitly hide the base class member, if it exists
        {
            return DateTime.Now > ExpirationDate;
        }

        // Additional methods
    }
}

