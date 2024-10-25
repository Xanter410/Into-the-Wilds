using Tools.BehaviorTree;
using UnityEngine;

namespace IntoTheWilds
{
    public class AI_MoveToTarget : Node
    {
        private Transform _transform;
        private IMove _treeMoveble;
        private float _deltaAroundTargetPosition;

        public AI_MoveToTarget(IMove movebleTree, Transform transform, float deltaAroundTargetPosition)
        {
            _treeMoveble = movebleTree;
            _transform = transform;
            _deltaAroundTargetPosition = deltaAroundTargetPosition;
        }

        public override NodeState Evaluate()
        {
            Transform target = (Transform)GetData("target");

            Vector2 moveDirection = Vector2.zero;

            float distanceToTarget = Vector2.Distance((Vector2)target.position, (Vector2)_transform.position);

            if (distanceToTarget <= _deltaAroundTargetPosition)
            {
                state = NodeState.SUCCESS;
            }
            else
            {
                moveDirection = ((Vector2)target.position - (Vector2)_transform.position).normalized;
                state = NodeState.RUNNING;
            }

            _treeMoveble.SetMoveInput(moveDirection);
            return state;
        }
    }
}
