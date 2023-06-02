using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using BehaviourTree;
using DG.Tweening;
using Controls;
using UnityEngine;

public class Attack : BehaviourNode
    // Behaviour node for the attack state of the eye. Increase paranoia meter.
{

    private EyeSight _eyeSight;
    private PlayerController _player;
    
    

    public Attack(EyeSight eyeSight, PlayerController player)
    {
        _eyeSight = eyeSight;
        _player = player;
    }

    public override NodeState _Evaluate()
    {
        // UnityEngine.Debug.Log("Attacking");

        if (_player.GetPlayerState() is DyingState)
            return NodeState.SUCCESS;

        if (_eyeSight.IsTargetInSight())
        {
            // increase heart rate
            HRGauge _heartRate = _player.GetHRGauge();

            // decrease max heart reate
            _heartRate.DecreaseMaxHR(_player, (float)GetData("decreaseMaxHRPerSecond"));

            if (_player.GetPlayerState() is DyingState)
                return NodeState.SUCCESS;

            return NodeState.RUNNING;
        }

        // lingering
        parent.parent.SetData("agitated", true);
        return NodeState.RUNNING;
    }
}
