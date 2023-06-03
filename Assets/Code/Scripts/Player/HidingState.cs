using System;
using UnityEngine;
using UnityEngine.Events;

namespace Controls {
    /// <summary>
    /// TODO: Make note of any music plugins we need here...
    /// </summary>
    public class HidingState : PlayerState
    {
        // TODO: Adjust multiplier values here
        private readonly float _heartRateMultiplier;
        
        public HidingState(float heartRateMultiplier)
        {
            _heartRateMultiplier = heartRateMultiplier;
        }
        
        public void OnEnter(PlayerState prevState, UnityEvent[] musicEvents)
        {
            // TODO: When the Player hides...what should happen? music? visual animations? Does it matter from which
            // state?
            // Selection of music events can be handled through indexing after events are input
            musicEvents[4]?.Invoke();
        }
        
        public float GetMovementSpeed()
        {
            return 0f;
        }
        
        public void Movement(Transform player, HRGauge heartRate, Action playerDeath)
        {
            // Increase the HR Gauge; note that this is called in a delegated method under update
            heartRate.IncreaseHR(playerDeath, _heartRateMultiplier);
            
            // TODO: Any special changes to how the player interacts with the environment while hiding?
        }
        
        public void OnExit(PlayerState newState, UnityEvent[] musicEvents)
        {
            // TODO: When the Player stops hiding...what should happen? music? visual animations? Does
            // it matter to which state?
            // Selection of music events can be handled through indexing after events are input
            musicEvents[5]?.Invoke();
        }
    }
}