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
        /// </summary>
        /// <param name="player"> The Player script calling this state.</param>
        /// /// <param name="rBody"> The Rigidbody to move.</param>
        ///  /// <param name="playerOffset"> The offset of the player object in the world.</param>
        /// <param name="baseSpeed"> The standard speed of the player gameobject.</param>
        abstract void Movement(PlayerController player, Rigidbody rBody, float baseSpeed);
    }
}
