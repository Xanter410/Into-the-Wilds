using IntoTheWilds.AI;
using System.Collections.Generic;
using Tools.BehaviorTree;
using UnityEngine;

namespace IntoTheWilds
{
    public class AI_MoveToTarget : Node
    {
        private Transform _transform;
        private IMove _treeMoveble;
        private float _deltaAroundTargetPosition;

        private List<Vector2> _pathToTarget = new();
        private Vector2 _oldMainTargetPosition;
        private Vector2 _subTarget;

        public AI_MoveToTarget(IMove movebleTree, Transform transform, float deltaAroundTargetPosition)
        {
            _treeMoveble = movebleTree;
            _transform = transform;
            _deltaAroundTargetPosition = deltaAroundTargetPosition;
        }

        public override NodeState Evaluate()
        {
            Vector2 moveDirection = Vector2.zero;

            Transform mainTarget = (Transform)GetData("target");

            float distanceToMainTarget = Vector2.Distance((Vector2)mainTarget.position, (Vector2)_transform.position);

            if (distanceToMainTarget <= _deltaAroundTargetPosition)
            {
                _pathToTarget.Clear();
                _subTarget = Vector2.zero;
                _oldMainTargetPosition = Vector2.zero;

                state = NodeState.SUCCESS;
                return state;
            }

            var deltaMainTargetPosition = _oldMainTargetPosition - (Vector2)mainTarget.position;

            if (deltaMainTargetPosition.x > 1 || deltaMainTargetPosition.y > 1 ||
                deltaMainTargetPosition.x < -1 || deltaMainTargetPosition.y < -1)
            {
                _pathToTarget = Pathfinding.FindPath(_transform.position, mainTarget.position);
                _oldMainTargetPosition = mainTarget.position;
                _subTarget = _pathToTarget[0];
            }

            if (_pathToTarget.Count > 1)
            {
                float distanceToSubTarget = Vector2.Distance(_subTarget, (Vector2)_transform.position);

                if (distanceToSubTarget < 0.15f)
                {
                    _ = _pathToTarget.Remove(_pathToTarget[0]);

                    _subTarget = _pathToTarget[0];
                }
            }
            else if (_pathToTarget.Count == 1)
            {
                moveDirection = ((Vector2)mainTarget.position - (Vector2)_transform.position).normalized;
                state = NodeState.RUNNING;

                _treeMoveble.SetMoveInput(moveDirection);
                return state;
            }
            else 
            {
                _pathToTarget = Pathfinding.FindPath(_transform.position, mainTarget.position);
                _oldMainTargetPosition = mainTarget.position;

                _subTarget = _pathToTarget[0];
            }

            moveDirection = (_subTarget - (Vector2)_transform.position).normalized;
            state = NodeState.RUNNING;

            _treeMoveble.SetMoveInput(moveDirection);
            return state;
        }
    }
}
