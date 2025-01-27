using UnityEngine;
using UnityEngine.Events;

namespace Tools.GameEvent
{
    public class GameEventBooleanListener : MonoBehaviour
    {
        public GameEventBoolean gameEvent;
        public UnityEvent<bool> onEventTriggered;

        void OnEnable()
        {
            gameEvent.AddListener(this);
        }
        void OnDisable()
        {
            gameEvent.RemoveListener(this);
        }
        public void OnEventTriggered(bool eventData)
        {
            onEventTriggered.Invoke(eventData);
        }
    }
}