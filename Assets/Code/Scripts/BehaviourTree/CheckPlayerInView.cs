using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

public class CheckPlayerInView : BehaviourNode
    // Node for checking if the player is in the view space of the eye. Should return a simple true/false within one evaluate call.
{
    private EyeSight _eyeSight;

    public CheckPlayerInView(EyeSight eyeSight)
    {
        _eyeSight = eyeSight;
    }

    public override NodeState _Evaluate()
    {
        if (_eyeSight.IsTargetInSight())
        {
            // detected oneshot trigger goes here!
            return NodeState.SUCCESS;
        }
            
        return NodeState.FAILURE;
    }
}
