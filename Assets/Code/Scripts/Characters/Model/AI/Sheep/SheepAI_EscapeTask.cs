using Tools.BehaviorTree;
using UnityEngine;

namespace IntoTheWilds
{
    public class SheepAI_EscapeTask : Node
    {
        private readonly SheepAI _tree;
        private readonly Transform _transform;

        public SheepAI_EscapeTask(SheepAI tree, Transform transform)
        {
            _tree = tree;
            _transform = transform;
        }

        public override NodeState Evaluate()
        {
            Transform target = (Transform)GetData("target");

            Vector2 moveDirection = Vector2.zero;

            if (Vector2.Distance(_transform.position, target.position) < 4f)
            {
                moveDirection = ((Vector2)_transform.position - (Vector2)target.position).normalized;
            }
            else
            {
                _ = ClearData("target");
            }

            ((IMove)_tree).SetMoveInput(moveDirection);

            state = NodeState.RUNNING;
            return state;
        }
    }
}
