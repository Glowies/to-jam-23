using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

public class CheckPlayerInNextRoom : BehaviourNode
    // Behaviour node for checking if the player is in the next room.
{
    // TODO
    public override NodeState _Evaluate()
    {
        return NodeState.FAILURE;
    }
}
