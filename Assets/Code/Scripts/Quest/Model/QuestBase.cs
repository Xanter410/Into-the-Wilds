using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace IntoTheWilds.Quest
{
    public interface IQuest
    {
        string QuestName { get; }
        string Description { get; }
        bool IsCompleted { get; }
        IReadOnlyList<IObjective> Objectives { get; }
        void Initialize();
        event Action<IQuest> OnQuestCompleted;
    }

    public class QuestBase : IQuest
    {
        public string QuestName { get; private set; }
        public string Description { get; private set; }
        public IReadOnlyList<IObjective> Objectives => _objectives.AsReadOnly();
        public bool IsCompleted => Objectives.All(o => o.IsCompleted);

        private List<IObjective> _objectives;

        public event Action<IQuest> OnQuestCompleted;

        public QuestBase(string questName, string description, List<IObjective> objectives)
        {
            QuestName = questName;
            Description = description;
            _objectives = objectives;

            foreach (var objective in _objectives)
            {
                objective.OnObjectiveCompleted += HandleObjectiveCompleted;
            }
        }

        public void Initialize()
        {
            foreach (var objective in _objectives)
            {
                objective.Initialize();
            }
        }

        private void HandleObjectiveCompleted(IObjective completedObjective)
        {
            if (IsCompleted)
            {
                OnQuestCompleted?.Invoke(this);
            }
        }
    }
}


