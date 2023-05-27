using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
    // behaviour tree template adapted from https://www.youtube.com/watch?v=aR6wt5BlE-E
{
    public abstract class Tree : MonoBehaviour
    {
        private BehaviourNode _root = null;
        protected bool isPaused = false;

        protected void Start()
        {
            _root = SetupTree();

            PauseManager.Instance.OnPauseToggled.AddListener((paused) =>
            {
                isPaused = paused;
            });
        }

        private void Update()
        {
            if (_root != null && !isPaused)
            {
                _root._Evaluate();
            }
        }

        protected abstract BehaviourNode SetupTree();

        protected object getRootData(string key)
        {
            if (_root == null) return null;
            return _root.GetData(key);
        }
    }
}
