namespace Tools.BehaviorTree
{
    public abstract class BehaviorTree
    {
        public Node RootNode => _root;
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
