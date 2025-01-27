using System.Collections.Generic;
using UnityEngine;

namespace Tools.GameEvent
{
    [CreateAssetMenu(fileName = "GameEvent_", menuName = "Game System/Game Event/Boolean")]
    public class GameEventBoolean : GameEvent<bool>
    {
        private readonly List<GameEventBooleanListener> listeners = new();

        public override void TriggerEvent(bool EventData)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventTriggered(EventData);
            }

            base.TriggerEvent(EventData);
        }

        public void AddListener(GameEventBooleanListener listener)
        {
            listeners.Add(listener);
        }
        public void RemoveListener(GameEventBooleanListener listener)
        {
            _ = listeners.Remove(listener);
        }
    }
}
