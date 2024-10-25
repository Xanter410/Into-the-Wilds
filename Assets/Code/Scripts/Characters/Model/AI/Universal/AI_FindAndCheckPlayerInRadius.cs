using Tools.BehaviorTree;
using UnityEngine;

namespace IntoTheWilds
{
    public class AI_FindAndCheckPlayerInRadius : Node
    {
        private LayerMask _playerLayerMask = LayerMask.GetMask("Player");
        private float _rangeAggress;
        private Transform _transform;

        public AI_FindAndCheckPlayerInRadius(Transform transform, float range)
        {
            _transform = transform;
            _rangeAggress = range;
        }

        public override NodeState Evaluate()
        {
            object target = GetData("target");

            if (target == null)
            {
                Collider2D collider2Ds = Physics2D.OverlapCircle(
                    (Vector2)_transform.position, _rangeAggress, _playerLayerMask);

                if (collider2Ds != null)
                {
                    SetDataRoot("target", collider2Ds.transform);
                    
                    state = NodeState.SUCCESS;
                    return state;
                }

                state = NodeState.FAILURE;
                return state;
            }
            else
            {
                Transform targetTransform = (Transform)target;

                float distanceToTarget = Vector2.Distance((Vector2)targetTransform.position, (Vector2)_transform.position);

                // ¬ проверке требуетс€ компенсаци€ разницы рассто€ни€
                // "до колизии Physics2D.OverlapCircle(...)"
                // и
                // "до центра таргета (distanceToTarget)"
                // ѕо этому к _rangeAggress добавл€етс€ некоторое рассто€ние.
                if (distanceToTarget <= (_rangeAggress + 1f))
                {
                    state = NodeState.SUCCESS;
                    return state;
                }

                ClearData("target");

                state = NodeState.FAILURE;
                return state;
            }
        }
    }
}
