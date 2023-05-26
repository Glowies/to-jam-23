using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

public class Attack : BehaviourNode
    // Behaviour node for the attack state of the eye. Increase paranoia meter.
{

    EyeSight _eyeSight;
    // TODO
    public Attack(EyeSight eyeSight)
    {
        _eyeSight = eyeSight;
    }

    public override NodeState _Evaluate()
    {

        if (_eyeSight.IsTargetInSight())
        {
            // animation trigger for attack goes here!


            _eyeSight.EyeTarget.ReceiveDamage();
            return NodeState.RUNNING;
        }


        // TODO: if player dies return success


        return NodeState.FAILURE;
    }
}
