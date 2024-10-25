using System.Collections;
using System.Collections.Generic;
using Tools.BehaviorTree;
using UnityEngine;

namespace IntoTheWilds
{
    public class AI_Attack : Node
    {
        private IAttack _tree;

        private float _delayBetweenAttackTime = 0f;
        private float _delayBetweenAttackCounter = 0f;

        public AI_Attack(IAttack tree, float delayBetweenAttackTime)
        {
            _tree = tree;
            _delayBetweenAttackTime = delayBetweenAttackTime;
        }

        public override NodeState Evaluate()
        {
            if (_delayBetweenAttackCounter <= 0)
            {
                _tree.AttackPressed();
                _delayBetweenAttackCounter = _delayBetweenAttackTime;
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
