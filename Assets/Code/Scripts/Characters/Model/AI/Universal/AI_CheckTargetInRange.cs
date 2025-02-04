using Tools.BehaviorTree;
using UnityEngine;

namespace IntoTheWilds
{
    public class AI_CheckTargetInRange : Node
    {
        private readonly Transform _transform;
        private readonly float _range;

        public AI_CheckTargetInRange(Transform transform, float range)
        {
            _transform = transform;
            _range = range;
        }

        public override NodeState Evaluate()
        {
            object target = GetData("target");

            Transform targetTransform = (Transform)target;

            float distanceToTarget = Vector2.Distance(
                (Vector2)targetTransform.position, 
                (Vector2)_transform.position);

            if (distanceToTarget <= _range)
            {
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}
