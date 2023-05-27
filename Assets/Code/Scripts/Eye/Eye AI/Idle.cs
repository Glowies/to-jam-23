using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

public class Idle : BehaviourNode
    // Behaviour node for when the eye is idling.
{
    private float _waitCounter = 0;

    public override NodeState _Evaluate()
    {
        // UnityEngine.Debug.Log("Idle");
        // might add an idle animation here?

        // get idle wait time from parent data
        if (_waitCounter < (float) GetData("idleWaitTime"))
        {
            // increment counter
            _waitCounter += UnityEngine.Time.deltaTime;
            return NodeState.RUNNING;
        }

        // end with success
        _waitCounter = 0;
        return NodeState.SUCCESS;
    }
}
