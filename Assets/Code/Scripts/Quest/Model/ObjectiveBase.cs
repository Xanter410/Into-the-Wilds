using System;
using UnityEngine;

namespace IntoTheWilds.Quest
{
    public interface IObjective
    {
        string Description { get; }
        bool IsCompleted { get; }
        void Initialize();
        void UpdateProgress(int amount);
        event Action<IObjective> OnObjectiveCompleted;
    }
    public abstract class ObjectiveBase : IObjective
    {
        public string Description { get; protected set; }
        public int TargetAmount { get; protected set; }
        public int CurrentAmount { get; protected set; }
        public bool IsCompleted => CurrentAmount >= TargetAmount;

        public event Action<IObjective> OnObjectiveCompleted;

        public ObjectiveBase(string description, int targetAmount)
        {
            Description = description;
            TargetAmount = targetAmount;
        }

        public virtual void Initialize()
        {
            CurrentAmount = 0;
        }

        public virtual void UpdateProgress(int amount)
        {
            if (IsCompleted) return;

            CurrentAmount += amount;
            if (IsCompleted)
            {
                OnObjectiveCompleted?.Invoke(this);
            }
        }
    }
}