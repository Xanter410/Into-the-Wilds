using Tools.BehaviorTree;

namespace IntoTheWilds
{
    public class AI_ClearTarget : Node
    {
        private readonly NodeState _returnValue;

        public AI_ClearTarget(NodeState returnValue) 
        { 
            _returnValue = returnValue;
        }

        public override NodeState Evaluate()
        {
            object target = GetData("target");
            if (target != null)
            {
                ClearData("target");
            }

            return _returnValue;
        }
    }
}
