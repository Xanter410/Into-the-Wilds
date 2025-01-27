using System;
using UnityEngine;

namespace Tools.GameEvent
{    
    public abstract class GameEvent<T> : ScriptableObject
    {
        private Action<T> _listenerActions;
        public virtual void TriggerEvent(T EventData)
        {
            _listenerActions?.Invoke(EventData);
        }
        public void AddListener(Action<T> callback)
        {
            _listenerActions += callback;
        }
        public void RemoveListener(Action<T> callback)
        {
            _listenerActions -= callback;
        }
    }
}
