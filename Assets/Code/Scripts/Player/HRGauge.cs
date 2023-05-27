using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class FloatUnityEvent : UnityEvent<float>
{
}

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
        public FloatUnityEvent OnHRIncrease = new FloatUnityEvent();
        // Do we want a separate visual effect for when the heart rate decreases? If not, then this can be merged
        // with the previous event...
        public FloatUnityEvent OnHRDecrease = new FloatUnityEvent();
        public FloatUnityEvent OnMaxHRDecrease = new FloatUnityEvent();
        
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
            _currentHR += increment * Time.deltaTime;

            // Handle GameOver when the HR goes over the threshold by delegating to player
            if (_currentHR > _maxThreshold)
            {
                _currentHR = _maxThreshold;
                playerDeath();
            }
            
            OnHRIncrease?.Invoke(_currentHR);
        }
        
        public void DecreaseHR(float decrement)
        {
            _currentHR -= decrement * Time.deltaTime;
            if (_currentHR < 0) _currentHR = 0;
            OnHRDecrease?.Invoke(_currentHR);
        }
        
        public void DecreaseMaxHR(Controls.PlayerController player, float decrement)
        {
            _maxThreshold -= decrement;
            OnMaxHRDecrease?.Invoke(_maxThreshold);

            if (_currentHR > _maxThreshold) player.Die();
        }
    }

}
