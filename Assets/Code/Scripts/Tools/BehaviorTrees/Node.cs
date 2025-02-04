using System.Collections.Generic;
using UnityEngine;

namespace Tools.BehaviorTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class Node
    {
        protected NodeState state;

        public Node parent;
        protected List<Node> children = new();

        private readonly Dictionary<string, object> _dataContext = new();

        public Node()
        {
            parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (Node child in children)
            {
                Attach(child);
            }
        }

        private void Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public void SetDataRoot(string key, object value)
        {
            Node node = parent;
            while (node.parent != null)
            {
                node = node.parent;
            }

            node.SetData(key, value);
        }

        public object GetData(string key)
        {
            if (_dataContext.TryGetValue(key, out object value))
            {
                return value;
            }

            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);

                if (value != null)
                {
                    return value;
                }

                node = node.parent;
            }

            return null;
        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _ = _dataContext.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);

                if (cleared)
                {
                    return true;
                }

                node = node.parent;
            }

            return false;
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;
    }
}