using System.Collections;
using System.Collections.Generic;
using System.Net;
using BehaviourTree;

public class CheckPlayerInView : BehaviourNode
    // Node for checking if the player is in the view space of the eye. If an attack is ongoing, this will return success. If the player moves away from view during an attack state, the attack will be ended.
{
    private EyeSight _eyeSight;
    private float _waitCounter = 0;
    private bool _waiting = true;

    public CheckPlayerInView(EyeSight eyeSight)
    {
        _eyeSight = eyeSight;
    }

    public override NodeState _Evaluate()
    {
        if (_eyeSight.IsTargetInSight())
        {
            if (_waiting)
            {
                _waitCounter += UnityEngine.Time.deltaTime;

                // if the player's been visible to the eye for a certain amount of time, start attack
                if (_waitCounter < (float)GetData("attackStartTime"))
                    return NodeState.RUNNING;
                else
                {
                    UnityEngine.Debug.Log("Player detected!");
                    // detected oneshot trigger goes here!

                    _waitCounter = 0;
                    _waiting = false;
                    return NodeState.SUCCESS;
                }
            } else
            {
                return NodeState.SUCCESS;
            }
            
        } else
        {
            UnityEngine.Debug.Log("Player not detected");
            _waiting = true;
            return NodeState.FAILURE;
        }
            
        
    }
}
