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
    private Transform _eyeTransform;
    

    public Attack(EyeSight eyeSight, PlayerController player, Transform transform)
    {
        _eyeSight = eyeSight;
        _player = player;
        _eyeTransform = transform;
    }

    public override NodeState _Evaluate()
    {
        // UnityEngine.Debug.Log("Attacking");

        if (_player.GetPlayerState() is DyingState)
            return NodeState.SUCCESS;

        if (_eyeSight.IsTargetInSight())
        {

            // get closer to window
            Room currRoom = (Room)GetData("currentRoom");

            if (GetData("currWindowIndex") == null)
            {
                // UnityEngine.Debug.LogError("Could not find window to attack through");
                return NodeState.FAILURE;
            }
            int currWindowIndex = (int)GetData("currWindowIndex");
            
            float eyeZOffset = (float)GetData("eyeWindowAttackingZOffset");
            _eyeTransform.DOMove(currRoom.Windows[currWindowIndex] + new Vector3(0, 0, eyeZOffset), (float)GetData("windowSwitchTime"));

            // animation trigger for attack goes here!


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
