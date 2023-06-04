using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using BehaviourTree;
using DG.Tweening;
using Controls;
using UnityEngine;
using UnityEngine.Events;

public class Attack : BehaviourNode
    // Behaviour node for the attack state of the eye. Increase paranoia meter.
{

    private EyeSight _eyeSight;
    private PlayerController _player;
    private readonly Light _selfLight;
    private readonly Light _windowLight;
    private UnityEvent<bool> _onAttackStateChange;
    private bool _isAttacking;
    

    public Attack(EyeSight eyeSight, PlayerController player, Light selfLight, Light windowLight, UnityEvent<bool> onAttackStateChange)
    {
        _eyeSight = eyeSight;
        _player = player;
        _selfLight = selfLight;
        _windowLight = windowLight;
        _onAttackStateChange = onAttackStateChange;
    }

    public override NodeState _Evaluate()
    {
        // UnityEngine.Debug.Log("Attacking");

        if (_player.GetPlayerState() is DyingState)
            return NodeState.SUCCESS;

        if (_eyeSight.IsTargetInSight())
        {

            if (GetData("currWindowIndex") == null)
            {
                // UnityEngine.Debug.LogError("Could not find window to attack through");
                return NodeState.FAILURE;
            }

            // increase heart rate
            HRGauge _heartRate = _player.GetHRGauge();

            // decrease max heart reate
            _heartRate.DecreaseMaxHR(_player, (float)GetData("decreaseMaxHRPerSecond"));
            
            // update light color
            _windowLight.intensity = (float) GetData("eyeAgitatedLightIntensity");
            _windowLight.color = (Color) GetData("eyeAgitatedColor");

            if (_player.GetPlayerState() is DyingState)
                return NodeState.SUCCESS;

            return NodeState.RUNNING;
        }

        if (_isAttacking) { 
            _isAttacking = false;
            _onAttackStateChange.Invoke(false);
        }

        // lingering
        parent.parent.SetData("agitated", true);
        
        return NodeState.RUNNING;
    }
}
