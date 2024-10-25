using Tools.BehaviorTree;
using UnityEngine;

namespace IntoTheWilds
{
    public class AI_CheckPlayerInRange : Node
    {
        private LayerMask _playerLayerMask = LayerMask.GetMask("Player");
        private float _range;

        private Transform _transform;

        public AI_CheckPlayerInRange(Transform transform, float range)
        {
            _transform = transform;
            _range = range;
        }

        public override NodeState Evaluate()
        {
            object target = GetData("target");

            Transform targetTransform = (Transform)target;

            if (Vector2.Distance((Vector2)targetTransform.position, (Vector2)_transform.position) <= _range)
            {
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}
