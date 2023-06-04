using System;
using UnityEngine;
using UnityEngine.Events;

namespace Controls
{
    /// <summary>
    /// Class that contains all data regarding the HeartRate Gauge that the player has throughout the game. Various
    /// methods are exposed to make use of this class when needed, such as IncreaseHR(), DecreaseHR(), DecreaseMaxHR()...
    /// TODO: Make note of any music plugins we need here...
    /// </summary>
    public class HRGauge
    {
        // ----------------- Events ------------------
        public UnityEvent<float> OnHRIncrease = new UnityEvent<float>();
        // Do we want a separate visual effect for when the heart rate decreases? If not, then this can be merged
        // with the previous event...
        public UnityEvent<float> OnHRDecrease = new UnityEvent<float>();
        public UnityEvent<float> OnMaxHRDecrease = new UnityEvent<float>();
        
        // --------------- Bookkeeping ---------------
        // TODO: Adjust threshold values here
        private float _maxThreshold = 100f;
        private float _currentHR = 0f;
    
        public float GetHR()
        {
            return _currentHR;
        }
        
        public void IncreaseHR(Action playerDeath, float increment)
        {
            _currentHR = Mathf.Clamp(_currentHR + increment * Time.deltaTime, 0f, _maxThreshold);
            OnHRIncrease?.Invoke(_currentHR);

            // Handle GameOver when the HR goes over the threshold by delegating to player
            if (_currentHR >= _maxThreshold) playerDeath();
        }
        
        public void DecreaseHR(float decrement)
        {
            _currentHR = Mathf.Clamp(_currentHR - decrement * Time.deltaTime, 0f, _maxThreshold);
            OnHRDecrease?.Invoke(_currentHR);
        }
        
        public void DecreaseMaxHR(PlayerController player, float decrement)
        {
            _maxThreshold -= decrement;
            OnMaxHRDecrease?.Invoke(_maxThreshold);

            if (_currentHR >= _maxThreshold) player.Die();
        }
    }

}
