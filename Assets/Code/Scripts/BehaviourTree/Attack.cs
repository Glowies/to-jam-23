using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using Controls;

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
        if (_player.GetPlayerState() is DyingState)
            return NodeState.SUCCESS;

        if (_eyeSight.IsTargetInSight())
        {
            // animation trigger for attack goes here!

            //_eyeSight.EyeTarget.ReceiveDamage();

            // increase heart rate
            HRGauge _heartRate = _player.GetHRGauge();
            // todo: add death method
            _heartRate.IncreaseHR(_player.Die, (float)GetData("damagePerSecond"));

            // decrease max heart reate
            _heartRate.DecreaseMaxHR(_player, (float)GetData("decreaseMaxHRPerSecond"));

            if (_player.GetPlayerState() is DyingState)
                return NodeState.SUCCESS;

            return NodeState.RUNNING;
        }


        // TODO: if player dies return success


        return NodeState.FAILURE;
    }
}
