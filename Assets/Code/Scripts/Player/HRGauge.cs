using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that contains all data regarding the HeartRate Gauge that the player has throughout the game. Various
/// methods are exposed to make use of this class when needed, such as IncreaseHR(), DecreaseHR(), DecreaseMaxHR()...
/// TODO: Make note of any music plugins we need here...
/// </summary>
public class HRGauge
{
    // --------------- Bookkeeping ---------------
    private float _maxThreshold;
    private float _currentHR;
    
    public float GetHR()
    {
        return _currentHR;
    }
    
    // TODO: Extend to update UI
    public void IncreaseHR(Controls.PlayerController player, float increment)
    {
        _currentHR += increment;
        Debug.Log(_currentHR);
        
        // Handle GameOver when the HR goes over the threshold by delegating to player
        if (_currentHR > _maxThreshold) player.Die();
    }
    
    // TODO: Extend to update UI
    public void DecreaseHR(Controls.PlayerController player, float decrement)
    {
        _currentHR -= decrement;
        if (_currentHR < 0) _currentHR = 0;
        Debug.Log(_currentHR);
    }
    
    // TODO: Extend to update UI
    public void DecreaseMaxHR(Controls.PlayerController player, float decrement)
    {
        _maxThreshold -= decrement;
        if (_currentHR > _maxThreshold) player.Die();
    }
}
