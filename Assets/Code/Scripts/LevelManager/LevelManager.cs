using UnityEngine;
using IntoTheWilds.Quest;

namespace IntoTheWilds
{
    public class LevelManager : MonoBehaviour
    {
        public QuestSystem QuestSystem { get; private set; }

        [SerializeField] private LevelData _currentLevelData;

        void Start()
        {
            QuestSystem = GetComponent<QuestSystem>();
            InitializeLevel();
        }

        private void InitializeLevel()
        {
            foreach (QuestData questData in _currentLevelData.Quests)
            {
                IQuest quest = QuestFactory.CreateQuest(questData);

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
            SceneTransition.SwitchToScene(_currentLevelData.NextLevelSceneBuildIndex);
        }
    }
}