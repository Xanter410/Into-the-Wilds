namespace Tools.BehaviorTree
{
    public abstract class BehaviorTree
    {
        public Node RootNode { get; private set; } = null;

        protected void Setup()
        {
            RootNode = SetupTree();
        }

        protected void Update()
        {
            _ = RootNode?.Evaluate();
        }

        protected abstract Node SetupTree();
    }
}
