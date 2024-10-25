using Tools.BehaviorTree;
using UnityEngine;

namespace IntoTheWilds
{
    public class AI_FindPlayerInRange : Node
    {
        private LayerMask _playerLayerMask = LayerMask.GetMask("Player");
        private float _range;

        private Transform _transform;

        public AI_FindPlayerInRange(Transform transform, float range)
        {
            _transform = transform;
            _range = range;
        }

        public override NodeState Evaluate()
        {
            object target = GetData("target");

            if (target == null) 
            {
                Collider2D collider2Ds = Physics2D.OverlapCircle(
                    (Vector2)_transform.position, _range, _playerLayerMask);

                if (collider2Ds != null) 
                {
                    SetDataRoot("target", collider2Ds.transform);

                    state = NodeState.SUCCESS;
                    return state;
                }

                state = NodeState.FAILURE;
                return state;
            }

            state = NodeState.SUCCESS;
            return state;
        }
    }
}
