using System;
using UnityEngine;

namespace Controls {
    /// <summary>
    /// TODO: Make note of any music plugins we need here...
    /// </summary>
    public class HidingState : PlayerState
    {
        private float _speedMultiplier = 0f;
        
        public void OnEnter(PlayerState prevState)
        {
            // TODO: When the Player hides...what should happen? music? visual animations? Does it matter from which
            // state?
        }
        
        public void Movement(PlayerController player, Rigidbody rBody, float baseSpeed)
        {
            // TODO: Any special changes to how the player interacts with the environment while hiding?
        }
        
        public void OnExit(PlayerState newState)
        {
            // TODO: When the Player stops hiding...what should happen? music? visual animations? Does
            // it matter to which state?
        }
    }
}