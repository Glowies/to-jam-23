using System;
using UnityEngine;

namespace Controls {
    /// <summary>
    /// TODO: Make note of any music plugins we need here...
    /// </summary>
    public class WalkingState : PlayerState
    {
        private float _speedMultiplier = 1f;
        
        public void OnEnter(PlayerState prevState)
        {
            // TODO: When the Player walks...what should happen? music? visual animations? Does it matter from which
            // state?
        }
        
        public void Movement(PlayerController player, Rigidbody rBody, float baseSpeed)
        {
            float speed = baseSpeed + _speedMultiplier;
            player.transform.Translate(Time.deltaTime * speed, 0, 0);
            
            // TODO: Any special changes to how the player interacts with the environment while walking?
        }
        
        public void OnExit(PlayerState newState)
        {
            // TODO: When the Player stops walking...what should happen? music? visual animations? Does
            // it matter to which state?
        }
    }
}