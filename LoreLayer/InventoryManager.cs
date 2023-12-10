using System;
using System.Collections.Generic;
using System.Linq;

namespace LoreLayer
{
    public class InventoryManager
    {
        private Dictionary<string, Item> items;
        public int Capacity { get; private set; }
        public int CurrentLoad { get; private set; }

        public InventoryManager(int capacity)
        {
            items = new Dictionary<string, Item>();
            Capacity = capacity;
            CurrentLoad = 0;
        }

        public bool AddItem(Item item)
        {
            int itemWeight = item.Weight * item.Quantity;
            if (CurrentLoad + itemWeight > Capacity)
                return false;

            if (items.ContainsKey(item.Id))
            {
                items[item.Id].Quantity += item.Quantity;
            }
            else
            {
                items.Add(item.Id, item);
            }

            CurrentLoad += itemWeight;
            return true;
        }

        public bool RemoveItem(string itemId, int quantity = 1)
        {
            if (items.ContainsKey(itemId) && items[itemId].Quantity >= quantity)
            {
                items[itemId].Quantity -= quantity;
                CurrentLoad -= items[itemId].Weight * quantity;

                if (items[itemId].Quantity <= 0)
                    items.Remove(itemId);

                return true;
            }
            return false;
        }

        public Item GetItem(string itemId)
        {
            return items.ContainsKey(itemId) ? items[itemId] : null;
        }

        public IEnumerable<Item> GetAllItems()
        {
            return items.Values;
        }

        public bool ContainsItem(string itemId)
        {
            return items.ContainsKey(itemId);
        }

        public IEnumerable<Item> FilterItemsByType(ItemType type)
        {
            return items.Values.Where(item => item.Type == type);
        }

        public IEnumerable<Item> FilterItemsByRarity(ItemRarity rarity)
        {
            return items.Values.Where(item => item.Rarity == rarity);
        }

        // Method to sort items based on various criteria (name, rarity, type, etc.)
        public IEnumerable<Item> SortItems(Func<Item, object> sortFunction)
        {
            return items.Values.OrderBy(sortFunction);
        }

        // Method to calculate the total value of items in the inventory
        public int CalculateTotalValue()
        {
            return items.Values.Sum(item => item.Value * item.Quantity);
        }

        // Method to check if the inventory is overburdened
        public bool IsOverburdened()
        {
            return CurrentLoad > Capacity;
        }

        // Method to resize the inventory capacity
        public void ResizeInventory(int newCapacity)
        {
            if (newCapacity >= CurrentLoad)
            {
                Capacity = newCapacity;
            }
            // Optionally, handle what happens if new capacity is less than current load
        }

        // Method to get the most valuable item in the inventory
        public Item GetMostValuableItem()
        {
            return items.Values.OrderByDescending(item => item.Value).FirstOrDefault();
        }

        // Method to get all items sorted by value
        public IEnumerable<Item> GetItemsSortedByValue()
        {
            return items.Values.OrderByDescending(item => item.Value);
        }

        // Method to check total number of a specific item type
        public int CountItemsOfType(ItemType itemType)
        {
            return items.Values.Count(item => item.Type == itemType);
        }

        // Method to apply a global modifier to all items (e.g., price increase, weight reduction)
        public void ApplyGlobalModifier(Func<Item, Item> modifierFunction)
        {
            foreach (var item in items.Values)
            {
                modifierFunction(item);
            }
        }

        // Method to merge similar items into stacks
        public void StackSimilarItems()
        {
            var groupedItems = items.Values.GroupBy(item => item.Id).Select(group => new Item(group.Key, group.First().Name, group.First().Description, group.Sum(g => g.Quantity), group.First().Weight, group.First().Value, group.First().Type, group.First().Rarity, group.First().IsTradable, group.First().IsConsumable));

            items = groupedItems.ToDictionary(item => item.Id, item => item);
            // Recalculate CurrentLoad if necessary
        }

        // Method to list items with a specific rarity
        public IEnumerable<Item> GetItemsByRarity(ItemRarity rarity)
        {
            return items.Values.Where(item => item.Rarity == rarity);
        }

        // Method to list expired items (if applicable, such as perishable goods)
        public IEnumerable<Item> GetExpiredItems()
        {
            return items.Values.Where(item => item is PerishableItem && ((PerishableItem)item).IsExpired());
        }

        // Method to remove all expired items from the inventory
        public void RemoveExpiredItems()
        {
            var expiredItems = GetExpiredItems().ToList();
            foreach (var item in expiredItems)
            {
                RemoveItem(item.Id, item.Quantity);
            }
        }

        // Additional inventory management methods
        // ...

        // Note: Due to the complexity and potential length of the code, more methods can be added as per specific game mechanics and requirements.
    }
}
