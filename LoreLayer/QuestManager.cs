using System;
using System.Collections.Generic;
using System.Linq;
using LoreLayer.Characters;

namespace LoreLayer
{
    public class QuestManager
    {
        private Dictionary<string, Quest> quests;
        private Dictionary<string, List<string>> playerQuestHistory; // Tracks completed quests for each player

        public QuestManager()
        {
            quests = new Dictionary<string, Quest>();
            playerQuestHistory = new Dictionary<string, List<string>>();
        }

        public void AddQuest(Quest quest)
        {
            quests[quest.Id] = quest;
        }

        public Quest GetQuest(string id)
        {
            return quests.TryGetValue(id, out var quest) ? quest : null;
        }

        public void StartQuest(string id, Character character)
        {
            if (!quests.TryGetValue(id, out var quest)) return;

            if (ArePrerequisitesMet(quest, character.Name) && quest.Status == QuestStatus.Available)
            {
                quest.Start();
                LogQuestStart(character.Name, id);
            }
        }

        public void UpdateQuests(Character character)
        {
            foreach (var quest in quests.Values.Where(q => q.Status == QuestStatus.Active))
            {
                quest.Update();
                if (quest.Status == QuestStatus.Completed)
                {
                    LogQuestCompletion(character.Name, quest.Id);
                }
            }
        }

        public IEnumerable<Quest> GetAvailableQuests(Character character)
        {
            return quests.Values.Where(quest => quest.Status == QuestStatus.Available && ArePrerequisitesMet(quest, character.Name));
        }

        private bool ArePrerequisitesMet(Quest quest, string characterName)
        {
            var completedQuests = playerQuestHistory.ContainsKey(characterName) ? playerQuestHistory[characterName] : new List<string>();
            return quest.Prerequisites.All(prerequisiteId => completedQuests.Contains(prerequisiteId));
        }

        private void LogQuestStart(string characterName, string questId)
        {
            if (!playerQuestHistory.ContainsKey(characterName))
            {
                playerQuestHistory[characterName] = new List<string>();
            }
        }

        private void LogQuestCompletion(string characterName, string questId)
        {
            if (!playerQuestHistory.TryGetValue(characterName, out var history))
            {
                history = new List<string>();
                playerQuestHistory[characterName] = history;
            }

            if (!history.Contains(questId))
            {
                history.Add(questId);
            }
        }
    }
}
