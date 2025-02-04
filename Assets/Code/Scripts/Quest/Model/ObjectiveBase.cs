using System;

namespace IntoTheWilds.Quest
{
    public interface IObjective
    {
        string Description { get; }
        int TargetAmount { get; }
        int CurrentAmount { get; }
        bool IsCompleted { get; }
        void Initialize();
        void UpdateProgress(int amount);
        event Action<IObjective> OnObjectiveCompleted;
        event Action OnObjectiveAmountChanged;
    }
    public abstract class ObjectiveBase : IObjective
    {
        public string Description { get; protected set; }
        public int TargetAmount { get; protected set; }
        public int CurrentAmount { get; protected set; }
        public bool IsCompleted => CurrentAmount >= TargetAmount;

        public event Action<IObjective> OnObjectiveCompleted;

        public event Action OnObjectiveAmountChanged;

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
            if (IsCompleted)
            {
                return;
            }

            CurrentAmount += amount;

            OnObjectiveAmountChanged?.Invoke();

            if (IsCompleted)
            {
                OnObjectiveCompleted?.Invoke(this);
            }
        }
    }
}