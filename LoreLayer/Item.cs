using System;

namespace LoreLayer
{
    public enum ItemType
    {
        Common,
        Consumable,
        Equipment,
        Quest,
        // Additional types as required
    }

    public enum ItemRarity
    {
        Normal,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        // Additional rarities as required
    }
    public class Item
    {
        public string Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Weight { get; set; }
        public int Value { get; set; }
        public ItemType Type { get; set; }
        public ItemRarity Rarity { get; set; }
        public bool IsTradable { get; set; }
        public bool IsConsumable { get; set; }
        public DateTime ExpirationDate { get; set; }
        public ItemEffect Effect { get; set; }

        public Item(string id, string name, string description, int quantity = 1, int weight = 1, int value = 0, ItemType type = ItemType.Common, ItemRarity rarity = ItemRarity.Normal, bool isTradable = true, bool isConsumable = false, DateTime? expirationDate = null, ItemEffect effect = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Quantity = quantity;
            Weight = weight;
            Value = value;
            Type = type;
            Rarity = rarity;
            IsTradable = isTradable;
            IsConsumable = isConsumable;
            ExpirationDate = expirationDate ?? DateTime.MaxValue;
            Effect = effect;
        }

        public void UseItem()
        {
            if (IsConsumable && Quantity > 0)
            {
                Effect?.Apply();
                Quantity--;
            }
        }

        public bool IsExpired()
        {
            return DateTime.Now > ExpirationDate;
        }

        // Clone method for duplicating the item (useful for item splitting, replication, etc.)
        public Item Clone()
        {
            return new Item(Id, Name, Description, Quantity, Weight, Value, Type, Rarity, IsTradable, IsConsumable, ExpirationDate, Effect);
        }

        // Method to update the item's value (could be influenced by game events, quests, etc.)
        public void UpdateValue(int newValue)
        {
            Value = newValue;
        }

        // Method to display item details (for UI elements or debugging)
        public override string ToString()
        {
            return $"Name: {Name}, Type: {Type}, Rarity: {Rarity}, Quantity: {Quantity}, Value: {Value}";
        }

        // Additional item properties and methods as required for your game's mechanics
        // ...

    }

    public class ItemEffect
    {
        // Define the effect properties and methods
        public Action EffectAction { get; set; }

        public ItemEffect(Action effectAction)
        {
            EffectAction = effectAction;
        }

        public void Apply()
        {
            EffectAction?.Invoke();
        }

        // Additional effect functionalities as needed
        // ...
    }
}
