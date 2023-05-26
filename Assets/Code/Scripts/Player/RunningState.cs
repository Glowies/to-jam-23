using System;
using UnityEngine;

namespace Controls {
    /// <summary>
    /// TODO: Make note of any music plugins we need here...
    /// </summary>
    public class RunningState : PlayerState
    {
        private float _speedMultiplier = 2f;
        
        public void OnEnter(PlayerState prevState)
        {
            // TODO: When the Player runs...what should happen? music? visual animations? Does it matter from which
            // state?
        }
        
        public void Movement(PlayerController player, Rigidbody rBody, float baseSpeed)
        {
            float speed = baseSpeed + _speedMultiplier;
            player.transform.Translate(Time.deltaTime * speed, 0, 0);
            
            // TODO: Any special changes to how the player interacts with the environment while running?
        }
        
        public void OnExit(PlayerState newState)
        {
            // TODO: When the Player stops running...what should happen? music? visual animations? Does
            // it matter to which state?
        }
    }
}