using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

public class LookThroughWindow : BehaviourNode
    // Behaviour node for when the eye is looking through a window.
{
    // TODO
    public override NodeState _Evaluate()
    {
        // looking through window animation trigger goes here!

        return NodeState.RUNNING;
    }
}
