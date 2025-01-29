using System;
using System.Collections.Generic;
using UnityEngine;

namespace IntoTheWilds.Quest
{
    public class QuestSystem : MonoBehaviour
    {
        public event Action OnAllQuestsCompleted;

        [SerializeField] private GameEventQuestProgress _OnQuestProggress;
        private List<IQuest> activeQuests = new List<IQuest>();
        

        private void OnEnable()
        {
            _OnQuestProggress.AddListener(UpdateObjectiveProgress);
        }

        private void OnDisable()
        {
            _OnQuestProggress.RemoveListener(UpdateObjectiveProgress);
        }

        public List<IQuest> GetListActiveQuests()
        {
            return activeQuests;
        }

        public void AddQuest(IQuest quest)
        {
            quest.OnQuestCompleted += HandleQuestCompleted;
            quest.Initialize();
            activeQuests.Add(quest);
        }

        public void UpdateObjectiveProgress(QuestProgressData progressData)
        {
            for (int i = activeQuests.Count - 1; i >= 0; i--)
            {
                var quest = activeQuests[i];

                foreach (var objective in quest.Objectives)
                {
                    if (objective is CollectObjective collectObjective &&
                        collectObjective.ResourceType == progressData.ResourceType)
                    {
                        collectObjective.UpdateProgress(progressData.Amount);
                    }

                    if (objective is KillObjective && progressData.ObjectiveType == ObjectiveType.Kill)
                    {
                        objective.UpdateProgress(progressData.Amount);
                    }
                }
            }

            CheckIsAllListQuestsCompleted();
        }

        private void CheckIsAllListQuestsCompleted()
        {
            if (activeQuests.Count == 0)
            {
                _OnQuestProggress.RemoveListener(UpdateObjectiveProgress);

                OnAllQuestsCompleted?.Invoke();
            }
        }

        private void HandleQuestCompleted(IQuest completedQuest)
        {
            Debug.Log($" вест завершЄн: {completedQuest.QuestName}");
            activeQuests.Remove(completedQuest);
        }

    }
}