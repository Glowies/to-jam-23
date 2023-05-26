using System;
using UnityEngine;

namespace Controls {
    /// <summary>
    /// Class used to declutter the PlayerController class, encapsulating the player's behaviour in each state
    /// to better reason about correctness and pinpoint bugs easily, plus specialize behaviours to specific states. 
    /// TODO: Make note of any music plugins we need here...
    /// </summary>
    public interface PlayerState
    {
        // Methods OnEnter and OnExit that can be extended for future purposes to encapsulate rendering, sound, and logic
        // that are associated with each state.
        // TODO: Consider what we need when switching states...reference to old state, to play audio, etc.
        public virtual void OnEnter(PlayerState prevState) {}
        public virtual void OnExit(PlayerState newState) {}

        /// <summary>
        /// Adjust the player movement speed and actions depending on the current state of the player.
        /// Walking: Regular player speed, reduces the heart rate gauge
        /// Running: Sped up running, but the heart rate gauge increases
        /// Hiding: Player does not move to avoid the eye's sight, but heart rate gauge increases
        /// Dying: The Player has lost the game, essentially ceasing all controls
        /// </summary>
        /// <param name="player"> The player's transform associated with the script calling this state.</param>
        /// <param name="heartRate"> The player's HRGauge associated with the script calling this state.</param>
        /// <param name="playerDeath"> Method to handle when the player dies.</param>
        /// <param name="baseSpeed"> The standard speed of the player gameobject.</param>
        abstract void Movement(Transform player, HRGauge heartRate, Action playerDeath, float baseSpeed);
    }
}
