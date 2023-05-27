using System;
using UnityEngine;

namespace Controls {
    /// <summary>
    /// TODO: Make note of any music plugins we need here...
    /// </summary>
    public class WalkingState : PlayerState
    {
        // TODO: Adjust multiplier values here
        private float _stateSpeed = 1f;
        private float _heartRateMultiplier = 10f;
        
        public void OnEnter(PlayerState prevState)
        {
            // TODO: When the Player walks...what should happen? music? visual animations? Does it matter from which
            // state?
        }
        
        public void Movement(Transform player, HRGauge heartRate, Action playerDeath, float baseSpeed)
        {
            float speed = baseSpeed + _stateSpeed;
            player.Translate(Time.deltaTime * speed, 0, 0);
            
            // Increase the HR Gauge; note that this is called in a delegated method under update
            heartRate.DecreaseHR(_heartRateMultiplier);
            
            // TODO: Any special changes to how the player interacts with the environment while walking?
        }
        
        public void OnExit(PlayerState newState)
        {
            // TODO: When the Player stops walking...what should happen? music? visual animations? Does
            // it matter to which state?
        }
    }
}