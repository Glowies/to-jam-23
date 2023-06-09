using System;
using UnityEngine;
using UnityEngine.Events;

namespace Controls {
    /// <summary>
    /// TODO: Make note of any music plugins we need here...
    /// </summary>
    public class DyingState : PlayerState
    {
        public void OnEnter(PlayerState prevState, UnityEvent[] musicEvents)
        {
            // TODO: When the Player dies...what should happen? music? visual animations? 
            // Does it depend on from what state you're coming from?
            // Should everything stop moving? Or just the player? And what should happen afterwards (UI)?
            // Selection of music events can be handled through indexing after events are input
            musicEvents[6]?.Invoke();
        }
        
        public float GetMovementSpeed()
        {
            return 0f;
        }
        
        public void Movement(Transform player, HRGauge heartRate, Action playerDeath)
        {
            // TODO: You're dead! So nothing happens unless we want to change this
        }
        
        // Note, once the player is dead, there is no exiting this state
    }
}
