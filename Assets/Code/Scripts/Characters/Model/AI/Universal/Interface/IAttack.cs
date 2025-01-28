using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IntoTheWilds
{
    public interface IAttack
    {
        public void AttackPressed();

        public void RegisterCallbackAttack(Action callbackHandler);

        public void UnRegisterCallbackAttack(Action callbackHandler);
    }
}