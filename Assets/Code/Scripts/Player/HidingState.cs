using System;
using UnityEngine;

namespace Controls {
    /// <summary>
    /// TODO: Make note of any music plugins we need here...
    /// </summary>
    public class HidingState : PlayerState
    {
        // TODO: Adjust multiplier values here
        private float _heartRateMultiplier = 3f;
        
        public void OnEnter(PlayerState prevState)
        {
            // TODO: When the Player hides...what should happen? music? visual animations? Does it matter from which
            // state?
        }
        
        public void Movement(Transform player, HRGauge heartRate, Action playerDeath, float baseSpeed)
        {
            // Increase the HR Gauge; note that this is called in a delegated method under update
            heartRate.IncreaseHR(playerDeath, _heartRateMultiplier);
            
            // TODO: Any special changes to how the player interacts with the environment while hiding?
        }
        
        public void OnExit(PlayerState newState)
        {
            // TODO: When the Player stops hiding...what should happen? music? visual animations? Does
            // it matter to which state?
        }
    }
}