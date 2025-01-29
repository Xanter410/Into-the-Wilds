using UnityEngine;
using System.Linq;
using IntoTheWilds.Quest;
namespace IntoTheWilds
{
    public class LevelManager : MonoBehaviour
    {
        public LevelData CurrentLevelData;
        public QuestSystem QuestSystem { get; private set; }

        void Awake()
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
            Debug.Log("��� ������ ���������");
            CompleteLevel();
        }

        private void CompleteLevel()
        {
            Debug.Log($"������� {CurrentLevelData.LevelName} ��������.");
            LoadNextLevel();
        }

        private void LoadNextLevel()
        {
            SceneTransition.SwitchToScene(CurrentLevelData.NextLevelSceneBuildIndex);
        }
    }
}