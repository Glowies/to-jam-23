using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
    // behaviour tree template adapted from https://www.youtube.com/watch?v=aR6wt5BlE-E
{
    public abstract class Tree : MonoBehaviour
    {
        private BehaviourNode _root = null;

        protected void Start()
        {
            _root = SetupTree();
        }

        private void Update()
        {
            if (_root != null)
            {
                _root._Evaluate();
            }
        }

        protected abstract BehaviourNode SetupTree();
    }
}
