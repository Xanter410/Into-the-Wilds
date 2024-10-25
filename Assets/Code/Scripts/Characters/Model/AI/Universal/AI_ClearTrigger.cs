using System.Collections;
using System.Collections.Generic;
using Tools.BehaviorTree;
using UnityEngine;

namespace IntoTheWilds
{
    public class AI_ClearTrigger : Node
    {
        public AI_ClearTrigger() { }

        public override NodeState Evaluate()
        {
            object target = GetData("target");
            if (target != null)
            {
                ClearData("target");
            }

            state = NodeState.SUCCESS;
            return state;
        }
    }
}
