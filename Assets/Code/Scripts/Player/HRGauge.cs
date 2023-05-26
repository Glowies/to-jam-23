using System;
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
    // TODO: Adjust threshold values here
    private float _maxThreshold = 100f;
    private float _currentHR = 0f;
    
    public float GetHR()
    {
        return _currentHR;
    }
    
    // TODO: Extend to update UI
    public void IncreaseHR(Action playerDeath, float increment)
    {
        _currentHR += increment * Time.deltaTime;

        // Handle GameOver when the HR goes over the threshold by delegating to player
        if (_currentHR > _maxThreshold) playerDeath();
    }
    
    // TODO: Extend to update UI
    public void DecreaseHR(float decrement)
    {
        _currentHR -= decrement * Time.deltaTime;
        if (_currentHR < 0) _currentHR = 0;
    }
    
    // TODO: Extend to update UI
    public void DecreaseMaxHR(Controls.PlayerController player, float decrement)
    {
        _maxThreshold -= decrement;
        if (_currentHR > _maxThreshold) player.Die();
    }
}
