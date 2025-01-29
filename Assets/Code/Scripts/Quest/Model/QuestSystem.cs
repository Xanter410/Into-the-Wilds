using System;
using System.Collections.Generic;
using UnityEngine;

namespace IntoTheWilds.Quest
{
    public class QuestSystem : MonoBehaviour
    {
        public event Action OnAllQuestsCompleted;
        public event Action<IQuest> OnNewActiveQuestAdded;

        [SerializeField] private GameEventQuestProgress _OnQuestProggress;

        private List<IQuest> _activeQuests = new();
        private List<IQuest> _queueQuests = new();
        private List<string> _complitedQuests = new();


        private void OnEnable()
        {
            _OnQuestProggress.AddListener(UpdateObjectiveProgress);
        }

        private void OnDisable()
        {
            _OnQuestProggress.RemoveListener(UpdateObjectiveProgress);
        }

        public void AddQuest(IQuest quest)
        {
            quest.OnQuestCompleted += HandleQuestCompleted;
            quest.Initialize();

            
            if (quest.ConditionsActivation.Count == 0 || CheckConditions(quest) == true)
            {
                _activeQuests.Add(quest);

                OnNewActiveQuestAdded?.Invoke(quest);
            }
            else
            {
                _queueQuests.Add(quest);
            }
        }

        public void UpdateObjectiveProgress(QuestProgressData progressData)
        {
            for (int i = _activeQuests.Count - 1; i >= 0; i--)
            {
                var quest = _activeQuests[i];

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

                    if (objective is FindObjective && progressData.ObjectiveType == ObjectiveType.Find)
                    {
                        objective.UpdateProgress(progressData.Amount);
                    }
                }
            }

            CheckIsAllListQuestsCompleted();
        }

        private void CheckIsAllListQuestsCompleted()
        {
            if (_activeQuests.Count == 0)
            {
                _OnQuestProggress.RemoveListener(UpdateObjectiveProgress);

                OnAllQuestsCompleted?.Invoke();
            }
        }

        private void HandleQuestCompleted(IQuest completedQuest)
        {
            _complitedQuests.Add(completedQuest.QuestName);
            _activeQuests.Remove(completedQuest);

            CheckQueueQuests();
        }

        private void CheckQueueQuests()
        {
            for (int i = _queueQuests.Count - 1; i >= 0; i--)
            {
                var quest = _queueQuests[i];

                if (CheckConditions(quest) == true)
                {
                    _activeQuests.Add(quest);

                    OnNewActiveQuestAdded?.Invoke(quest);

                    _queueQuests.Remove(_queueQuests[i]);
                }
            }
        }

        private bool CheckConditions(IQuest quest)
        {
            bool IsConditionsTrue = false;

            foreach (var Condition in quest.ConditionsActivation)
            {
                if (_complitedQuests.Contains(Condition))
                {
                    IsConditionsTrue = true;
                }
                else
                {
                    IsConditionsTrue = false;
                }
            }

            return IsConditionsTrue;
        }
    }
}