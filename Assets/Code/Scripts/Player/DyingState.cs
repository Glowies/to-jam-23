using System;
using UnityEngine;

namespace Controls {
    /// <summary>
    /// TODO: Make note of any music plugins we need here...
    /// </summary>
    public class DyingState : PlayerState
    {
        public void OnEnter(PlayerState prevState)
        {
            // TODO: When the Player dies...what should happen? music? visual animations? 
            // Does it depend on from what state you're coming from?
            // Should everything stop moving? Or just the player? And what should happen afterwards (UI)?
            
            // For now, everything will stop moving
            Time.timeScale = 0;
            
            // TODO: Spawn some game over menu
        }
        
        public void Movement(Transform player, HRGauge heartRate, Action playerDeath, float baseSpeed)
        {
            // TODO: You're dead! So nothing happens unless we want to change this
        }
        
        // Note, once the player is dead, there is no exiting this state
    }
}
