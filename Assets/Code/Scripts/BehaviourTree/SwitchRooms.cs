using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

public class SwitchRooms : BehaviourNode
    // Room switching behaviour for the eye. Should fail if the player hasn't switched rooms since the last evaluation of this node.
    // Otherwise, move the eye to the next room and update data on the root about which room the eye is in.
{
    // TODO
    public override NodeState _Evaluate()
    {
        return NodeState.FAILURE;
    }
}
