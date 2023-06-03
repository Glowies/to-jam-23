using System;
using UnityEngine;
using UnityEngine.Events;

namespace Controls {
    /// <summary>
    /// TODO: Make note of any music plugins we need here...
    /// </summary>
    public class RunningState : PlayerState
    {
        // TODO: Adjust multiplier values here
        private readonly float _stateSpeed;
        private readonly float _stateSpeedBlendDuration;
        private readonly float _heartRateMultiplier;
        
        private float _currentSpeed, _t;

        public RunningState(float stateSpeed, float stateSpeedBlendDuration, float heartRateMultiplier)
        {
            _stateSpeed = stateSpeed;
            _stateSpeedBlendDuration = stateSpeedBlendDuration;
            _heartRateMultiplier = heartRateMultiplier;
            
            _currentSpeed = stateSpeed;
        }
        
        public void OnEnter(PlayerState prevState, UnityEvent[] musicEvents)
        {
            // TODO: When the Player runs...what should happen? music? visual animations? Does it matter from which
            // state?
            // Selection of music events can be handled through indexing after events are input
            musicEvents[2]?.Invoke();
            
            if (prevState is WalkingState || prevState is HidingState)
            {
                // Smooth running or hiding speed to walking speed
                _currentSpeed = prevState.GetMovementSpeed();
                _t = 0;
            }
        }
        
        public float GetMovementSpeed()
        {
            return _currentSpeed;
        }
        
        public void Movement(Transform player, HRGauge heartRate, Action playerDeath)
        {
            if (_t < 1)
            {
                _currentSpeed = Mathf.SmoothStep(_currentSpeed, _stateSpeed, _t / _stateSpeedBlendDuration);
                _t += Time.deltaTime;
            }

            player.Translate(Time.deltaTime * _currentSpeed, 0, 0);
            
            // Increase the HR Gauge; note that this is called in a delegated method under update
            heartRate.IncreaseHR(playerDeath, _heartRateMultiplier);
            
            // TODO: Any special changes to how the player interacts with the environment while running?
        }
        
        public void OnExit(PlayerState newState, UnityEvent[] musicEvents)
        {
            // TODO: When the Player stops running...what should happen? music? visual animations? Does
            // it matter to which state?
            musicEvents[3]?.Invoke();
        }
    }
}