using UnityEngine;
using IntoTheWilds.Quest;

namespace IntoTheWilds
{
    public class LevelManager : MonoBehaviour
    {
        public LevelData CurrentLevelData;
        public QuestSystem QuestSystem { get; private set; }

        void Start()
        {
            QuestSystem = GetComponent<QuestSystem>();
            InitializeLevel();
        }

        private void InitializeLevel()
        {
            foreach (var questData in CurrentLevelData.Quests)
            {
                var quest = QuestFactory.CreateQuest(questData);

                QuestSystem.AddQuest(quest);
            }

            QuestSystem.OnAllQuestsCompleted += HandleAllQuestsCompletion;
        }

        private void HandleAllQuestsCompletion()
        {
            CompleteLevel();
        }

        private void CompleteLevel()
        {
            LoadNextLevel();
        }

        private void LoadNextLevel()
        {
            SceneTransition.SwitchToScene(CurrentLevelData.NextLevelSceneBuildIndex);
        }
    }
}