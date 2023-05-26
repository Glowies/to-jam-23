using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
    // behaviour tree template adapted from https://www.youtube.com/watch?v=aR6wt5BlE-E
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
    public class BehaviourNode
        // each node has access to its parent and children, and any data that we might pass on
    {
        protected NodeState state;

        public BehaviourNode parent;
        protected List<BehaviourNode> children = new List<BehaviourNode>();

        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        public BehaviourNode()
            // empty node
        {
            parent = null;
        }

        public BehaviourNode(List<BehaviourNode> children)
        {
            foreach (BehaviourNode child in children)
                _Attach(child);
        }

        private void _Attach(BehaviourNode node)
        {
            node.parent = this;
            children.Add(node);
        }

        // this method will be called each update.
        public virtual NodeState _Evaluate() => NodeState.RUNNING;

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
            // get data from key, looking further back in the tree until we find it or reach the root.
        {
            object value = null;

            if (_dataContext.TryGetValue(key, out value))
                return value;

            BehaviourNode currNode = parent;
            while (currNode != null)
            {
                value = currNode.GetData(key);
                if (value != null)
                    return value;
                currNode = currNode.parent;
            }
            return null;
        }

        public bool ClearData(string key)
            // same as get data but for clearing data.
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            BehaviourNode currNode = parent;
            while (currNode != null)
            {
                bool cleared = currNode.ClearData(key);
                if (cleared)
                    return true;
                currNode = currNode.parent;
            }

            return false;
                
        }
    }
}

