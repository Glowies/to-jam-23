using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

public class Idle : BehaviourNode
    // Behaviour node for when the eye is idle.
{
    // TODO
    public override NodeState _Evaluate()
    {
        return NodeState.FAILURE;
    }
}
