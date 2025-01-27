using UnityEngine;
using System.Linq;
using IntoTheWilds.Quest;
namespace IntoTheWilds
{
    public class LevelManager : MonoBehaviour
    {
        public LevelData CurrentLevelData;
        private QuestSystem questSystem;

        void Start()
        {
            questSystem = GetComponent<QuestSystem>();
            InitializeLevel();
        }

        private void InitializeLevel()
        {
            foreach (var questData in CurrentLevelData.Quests)
            {
                var quest = QuestFactory.CreateQuest(questData);

                questSystem.AddQuest(quest);
            }

            questSystem.OnAllQuestsCompleted += HandleAllQuestsCompletion;
        }

        private void HandleAllQuestsCompletion()
        {
            Debug.Log("Все квесты завершены");
            CompleteLevel();
        }

        private void CompleteLevel()
        {
            Debug.Log($"Уровень {CurrentLevelData.LevelName} завершён.");
            LoadNextLevel();
        }

        private void LoadNextLevel()
        {
            SceneTransition.SwitchToScene(CurrentLevelData.NextLevelSceneBuildIndex);
        }
    }
}