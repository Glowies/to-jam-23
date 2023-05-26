using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

public class CheckPlayerInView : BehaviourNode
    // Node for checking if the player is in the view space of the eye. Should return a simple true/false within one evaluate call.
{
    // TODO
    public override NodeState _Evaluate()
    {
        return NodeState.FAILURE;
    }
}
