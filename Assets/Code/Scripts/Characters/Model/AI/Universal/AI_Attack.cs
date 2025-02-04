using Tools.BehaviorTree;
using UnityEngine;

namespace IntoTheWilds
{
    public class AI_Attack : Node
    {
        private readonly IAttack _tree;

        private readonly float _delayBetweenAttack = 0f;
        private float _delayBetweenAttackCounter = 0f;

        public AI_Attack(IAttack tree, float delayBetweenAttackInSeconds)
        {
            _tree = tree;
            _delayBetweenAttack = delayBetweenAttackInSeconds;
        }

        public override NodeState Evaluate()
        {
            if (_delayBetweenAttackCounter <= 0)
            {
                _delayBetweenAttackCounter = _delayBetweenAttack;

                _tree.OnAttackPressed();
            }
            else 
            {
                _delayBetweenAttackCounter -= Time.deltaTime;
            }

            state = NodeState.RUNNING;
            return state;
        }
    }
}
