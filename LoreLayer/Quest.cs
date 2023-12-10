using System;
using System.Collections.Generic;
using LoreLayer.Characters;

namespace LoreLayer
{
    public class Quest
    {
        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public QuestStatus Status { get; private set; }
        public List<QuestObjective> Objectives { get; private set; }
        public QuestReward Reward { get; private set; }
        public List<string> Prerequisites { get; private set; }
        public List<QuestDialogue> Dialogues { get; private set; }
        private Character character; // Assume a character is associated with the quest
        private CurrencyManager currencyManager; // Assume a currency manager is available

        public Quest(string id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
            Status = QuestStatus.Available;
            Objectives = new List<QuestObjective>();
            Prerequisites = new List<string>();
            Dialogues = new List<QuestDialogue>();
        }

        public void AddObjective(QuestObjective objective)
        {
            Objectives.Add(objective);
        }

        public void AddPrerequisite(string questId)
        {
            Prerequisites.Add(questId);
        }

        public void AddDialogue(QuestDialogue dialogue)
        {
            Dialogues.Add(dialogue);
        }

        public void Start()
        {
            if (Status == QuestStatus.Available)
            {
                Status = QuestStatus.Active;
            }
        }

        public void Update()
        {
            if (Status != QuestStatus.Active) return;

            bool allObjectivesComplete = true;
            foreach (var objective in Objectives)
            {
                if (!objective.IsComplete)
                {
                    allObjectivesComplete = false;
                    break;
                }
            }

            if (allObjectivesComplete)
            {
                Complete();
            }
        }

        public void Complete()
        {
            if (Reward != null)
            {
                Reward.Grant(character, currencyManager);
            }
            Status = QuestStatus.Completed;
        }

        public void Fail()
        {
            Status = QuestStatus.Failed;
        }
    }

    public enum QuestStatus
    {
        Available,
        Active,
        Completed,
        Failed
    }

    public class QuestObjective
    {
        public string Description { get; private set; }
        public bool IsComplete { get; private set; }

        public QuestObjective(string description)
        {
            Description = description;
        }

        public void Complete()
        {
            IsComplete = true;
        }
    }

    public class QuestDialogue
    {
        public string Text { get; set; }
        public Dictionary<int, DialogueOption> Options { get; set; }

        public QuestDialogue(string text)
        {
            Text = text;
            Options = new Dictionary<int, DialogueOption>();
        }

        public void AddOption(int optionNumber, DialogueOption option)
        {
            Options[optionNumber] = option;
        }

        public void Display()
        {
            Console.WriteLine(Text);
        }
    }

    public class DialogueOption
    {
        public string Description { get; set; }
        public Action<Quest> Action { get; set; }

        public DialogueOption(string description, Action<Quest> action)
        {
            Description = description;
            Action = action;
        }

        public void Execute(Quest quest)
        {
            Action?.Invoke(quest);
        }
    }

    public class QuestReward
    {
        public List<Item> Items { get; }
        public int ExperiencePoints { get; private set; }
        public Dictionary<string, decimal> CurrencyRewards { get; private set; }
        public List<StatModifier> StatModifiers { get; }

        public QuestReward()
        {
            Items = new List<Item>();
            StatModifiers = new List<StatModifier>();
            CurrencyRewards = new Dictionary<string, decimal>();
        }

        public void AddItemReward(Item item)
        {
            Items.Add(item);
        }

        public void SetExperienceReward(int xp)
        {
            ExperiencePoints = xp;
        }

        public void AddCurrencyReward(string currencyId, decimal amount)
        {
            if (CurrencyRewards.ContainsKey(currencyId))
                CurrencyRewards[currencyId] += amount;
            else
                CurrencyRewards.Add(currencyId, amount);
        }

        public void AddStatModifier(StatModifier modifier)
        {
            StatModifiers.Add(modifier);
        }

        public void Grant(Character character, CurrencyManager currencyManager)
        {
            foreach (var item in Items)
                character.Inventory.AddItem(item);

            character.Stats.AddExperience(ExperiencePoints);

            foreach (var currencyReward in CurrencyRewards)
                currencyManager.AddToCurrency(currencyReward.Key, currencyReward.Value);

            foreach (var modifier in StatModifiers)
                modifier.Apply(character.Stats);
        }
    }
}
