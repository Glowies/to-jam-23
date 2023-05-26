using System;
using UnityEngine;

namespace Controls {
    /// <summary>
    /// TODO: Make note of any music plugins we need here...
    /// </summary>
    public class RunningState : PlayerState
    {
        // TODO: Adjust multiplier values here
        private float _speedMultiplier = 2f;
        private float _heartRateMultiplier = 3f;
        
        public void OnEnter(PlayerState prevState)
        {
            // TODO: When the Player runs...what should happen? music? visual animations? Does it matter from which
            // state?
        }
        
        public void Movement(Transform player, HRGauge heartRate, Action playerDeath, float baseSpeed)
        {
            float speed = baseSpeed + _speedMultiplier;
            player.Translate(Time.deltaTime * speed, 0, 0);
            
            // Increase the HR Gauge; note that this is called in a delegated method under update
            heartRate.IncreaseHR(playerDeath, _heartRateMultiplier);
            
            // TODO: Any special changes to how the player interacts with the environment while running?
        }
        
        public void OnExit(PlayerState newState)
        {
            // TODO: When the Player stops running...what should happen? music? visual animations? Does
            // it matter to which state?
        }
    }
}