using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

public class CheckPlayerInView : BehaviourNode
    // Node for checking if the player is in the view space of the eye. Should return a simple true/false within one evaluate call.
{
    private EyeSight _eyeSight;
    private float _waitCounter = 0;

    public CheckPlayerInView(EyeSight eyeSight)
    {
        _eyeSight = eyeSight;
    }

    public override NodeState _Evaluate()
    {
        if (_eyeSight.IsTargetInSight())
        {
            _waitCounter += UnityEngine.Time.deltaTime;

            // if the player's been visible to the eye for a certain amount of time, start attack
            if (_waitCounter < (float) GetData("attackStartTime"))
                return NodeState.RUNNING;
            else
            {
                // detected oneshot trigger goes here!

                _waitCounter = 0;
                return NodeState.SUCCESS;
            }
        } else
        {
            return NodeState.FAILURE;
        }
            
        
    }
}
