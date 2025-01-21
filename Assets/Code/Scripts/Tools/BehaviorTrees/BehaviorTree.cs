namespace Tools.BehaviorTree
{
    public abstract class BehaviorTree
    {
        private Node _root = null;

        protected void Setup()
        {
            _root = SetupTree();
        }

        protected void Update()
        {
            _root?.Evaluate();
        }

        protected abstract Node SetupTree();
    }
}
