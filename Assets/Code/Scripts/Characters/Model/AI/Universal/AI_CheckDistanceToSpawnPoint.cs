using Tools.BehaviorTree;
using UnityEngine;

namespace IntoTheWilds
{
    public class AI_CheckDistanceToSpawnPoint : Node
    {
        private readonly Transform _transform;
        private readonly Vector2 _spawnPoint;

        private readonly float _maxDistanceMove;
        private readonly float _maxDistanceAggress;

        private bool _isComebackMove = false;

        public AI_CheckDistanceToSpawnPoint(
            Transform transform, 
            Vector2 spawnPoint,
            float maxDistanceMove,
            float maxDistanceAggress
            )
        {
            _transform = transform;
            _spawnPoint = spawnPoint;
            _maxDistanceMove = maxDistanceMove;
            _maxDistanceAggress = maxDistanceAggress;
        }

        public override NodeState Evaluate()
        {
            float distanceToSpawn = Vector2.Distance((Vector2)_transform.position, _spawnPoint);

            if (distanceToSpawn <= _maxDistanceMove && !_isComebackMove)
            {
                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                if (distanceToSpawn > _maxDistanceAggress) 
                {
                    _isComebackMove = true;

                    state = NodeState.FAILURE;
                    return state;
                }
                else
                {
                    _isComebackMove = false;

                    state = NodeState.SUCCESS;
                    return state;
                }
            }
        }
    }
}
