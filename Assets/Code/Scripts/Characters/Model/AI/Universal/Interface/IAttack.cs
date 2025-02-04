using System;

namespace IntoTheWilds
{
    public interface IAttack
    {
        public event Action AttackPressed;

        public void OnAttackPressed();
    }
}