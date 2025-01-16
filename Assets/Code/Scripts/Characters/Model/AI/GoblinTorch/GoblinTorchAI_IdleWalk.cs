using IntoTheWilds;
using Tools.BehaviorTree;
using UnityEngine;

namespace IntoTheWilds
{
    public class GoblinTorchAI_IdleWalk : Node
    {
        private GoblinTorchAI _tree;
        private Transform _transform;
        private Vector2 _spawnPoint;

        private float _walkRadius = 3f;
        private float _waitTime = 3f;
        private float _waitCounter = 0f;
        private bool _isWaiting = false;
        private bool _isTargetSet = false;
        private Vector2 _target = Vector2.zero;

        public GoblinTorchAI_IdleWalk(GoblinTorchAI tree, Transform transform, Vector2 spawnPoint)
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
                }
            }
            else
            {
                if (!_isTargetSet)
                {
                    _target = GetRandomTargetInRadius();
                    _isTargetSet = true;
                }

                if (Vector2.Distance(_transform.position, _target) < 0.5f)
                {
                    _waitCounter = 0f;
                    _isWaiting = true;
                    _isTargetSet = false;
                }
                else
                {
                    moveDirection = (_target - (Vector2)_transform.position).normalized;
                }
            }

            _tree.SetMoveInput(moveDirection);

            state = NodeState.RUNNING;
            return state;
        }

        private Vector3 GetRandomTargetInRadius()
        {
            Vector3 target = Vector3.zero;

            target.x = Random.Range(
                _spawnPoint.x - (_walkRadius / 2),
                _spawnPoint.x + (_walkRadius / 2));

            target.y = Random.Range(
                _spawnPoint.y - (_walkRadius / 2),
                _spawnPoint.y + (_walkRadius / 2));

            return target;
        }
    }
}