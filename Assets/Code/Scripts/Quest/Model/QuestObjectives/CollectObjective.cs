namespace IntoTheWilds.Quest
{
    public class CollectObjective : ObjectiveBase
    {
        public ResourceType ResourceType { get; private set; }

        public CollectObjective(string description, int targetAmount, ResourceType resourceType) : base(description, targetAmount)
        {
            ResourceType = resourceType;
        }
    }
}

