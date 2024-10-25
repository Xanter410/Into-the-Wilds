using UnityEngine;

namespace IntoTheWilds
{
    public interface IMove
    {
        public Vector2 RetrieveMoveInput();
        public void SetMoveInput(Vector2 moveDirection);
    }
}
