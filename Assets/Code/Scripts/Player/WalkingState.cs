using System;
using UnityEngine;
using UnityEngine.Events;

namespace Controls {
    /// <summary>
    /// TODO: Make note of any music plugins we need here...
    /// </summary>
    public class WalkingState : PlayerState
    {
        // TODO: Adjust multiplier values here
        private readonly float _stateSpeed;
        private readonly float _stateSpeedBlendDuration;
        private readonly float _heartRateMultiplier;
        
        private float _currentSpeed, _t;

        public WalkingState(float stateSpeed, float stateSpeedBlendDuration, float heartRateMultiplier)
        {
            _stateSpeed = stateSpeed;
            _stateSpeedBlendDuration = stateSpeedBlendDuration;
            _heartRateMultiplier = heartRateMultiplier;

            _currentSpeed = stateSpeed;
        }
        
        public void OnEnter(PlayerState prevState, UnityEvent[] musicEvents)
        {
            // TODO: When the Player walks...what should happen? music? visual animations? Does it matter from which
            // state?
            // Selection of music events can be handled through indexing after events are input
            // Currently the indexes are hard-coded but can be fixed when the music events are finalized
            musicEvents[0]?.Invoke();
            
            if (prevState is RunningState || prevState is HidingState)
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
            if (_t < _stateSpeedBlendDuration)
            {
                _currentSpeed = Mathf.SmoothStep(_currentSpeed, _stateSpeed, _t / _stateSpeedBlendDuration);
                _t += Time.deltaTime;
            }

            player.Translate(Time.deltaTime * _currentSpeed, 0, 0);
            
            // Increase the HR Gauge; note that this is called in a delegated method under update
            heartRate.DecreaseHR(_heartRateMultiplier);
            
            // TODO: Any special changes to how the player interacts with the environment while walking?
        }
        
        public void OnExit(PlayerState newState, UnityEvent[] musicEvents)
        {
            // TODO: When the Player stops walking...what should happen? music? visual animations? Does
            // it matter to which state?
            
            musicEvents[1]?.Invoke();
        }
    }
}