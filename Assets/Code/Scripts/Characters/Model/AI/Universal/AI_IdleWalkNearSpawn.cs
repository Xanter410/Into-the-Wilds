using IntoTheWilds.AI;
using System.Collections.Generic;
using Tools.BehaviorTree;
using UnityEngine;

namespace IntoTheWilds
{
    public class AI_IdleWalkNearSpawn : Node
    {
        private IMove _tree;
        private Transform _transform;
        private Vector2 _spawnPoint;

        private float _walkRadius = 3f;
        private float _waitTime = 3f;
        private float _waitCounter = 0f;

        private bool _isWaiting = false;
        private bool _isTargetSet = false;

        private Vector2 _mainTarget = Vector2.zero;
        private List<Vector2> _pathToTarget = new();
        private Vector2 _subTarget;

        public AI_IdleWalkNearSpawn(IMove tree, Transform transform, Vector2 spawnPoint)
        {
            _tree = tree;
            _transform = transform;
            _spawnPoint = spawnPoint;
        }

        public override NodeState Evaluate()
        {
            Vector2 moveDirection = Vector2.zero;

            if (_isWaiting)
            {
                _waitCounter += Time.deltaTime;

                if (_waitCounter >= _waitTime)
                {
                    _isWaiting = false;
                    _waitCounter = 0f;
                }
            }
            else
            {
                if (_isTargetSet == false)
                {
                    GetRandomTargetInRadius();
                    _subTarget = _pathToTarget[0];
                    _isTargetSet = true;
                }

                if (Vector2.Distance(_transform.position, _mainTarget) < 0.5f)
                {
                    // ќт текущего положени€ до цели меньше половины клетки.
                    // —читаем что главна€ цель достигнута.

                    _pathToTarget.Clear();
                    _mainTarget = Vector2.zero;
                    _subTarget = Vector2.zero;

                    _isWaiting = true;
                    _isTargetSet = false;
                }
                else
                {
                    var distanceToSubTarget = Vector2.Distance(_subTarget, (Vector2)_transform.position);

                    if (distanceToSubTarget < 0.15f)
                    {
                        _ = _pathToTarget.Remove(_pathToTarget[0]);

                        if (_pathToTarget.Count > 0)
                        {
                            _subTarget = _pathToTarget[0];
                            moveDirection = (_subTarget - (Vector2)_transform.position).normalized;
                        }
                        else
                        {
                            // ÷ель маршрута _pathToTarget достигнута.
                            // —читаем что обща€ цель тоже достигнута.

                            _mainTarget = Vector2.zero;
                            _subTarget = Vector2.zero;

                            _isWaiting = true;
                            _isTargetSet = false;
                        }
                    }
                    else
                    {
                        moveDirection = (_subTarget - (Vector2)_transform.position).normalized;
                    }

                    state = NodeState.RUNNING;
                }
            }

            _tree.SetMoveInput(moveDirection);

            state = NodeState.RUNNING;
            return state;
        }

        private void GetRandomTargetInRadius()
        {
            do
            {
                Vector2 target = Vector2.zero;

                target.x = Random.Range(
                    _spawnPoint.x - (_walkRadius / 2),
                    _spawnPoint.x + (_walkRadius / 2));

                target.y = Random.Range(
                    _spawnPoint.y - (_walkRadius / 2),
                    _spawnPoint.y + (_walkRadius / 2));

                _pathToTarget = Pathfinding.FindPath(_transform.position, target);
                _mainTarget = target;

            } while (_pathToTarget == null);
        }
    }
}