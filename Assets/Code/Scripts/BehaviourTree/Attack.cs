using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

public class Attack : BehaviourNode
    // Behaviour node for the attack state of the eye. Increase paranoia meter.
{
    // TODO
    public override NodeState _Evaluate()
    {
        return NodeState.FAILURE;
    }
}
