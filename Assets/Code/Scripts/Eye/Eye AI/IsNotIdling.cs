using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using DG.Tweening;

public class IsNotIdling : BehaviourNode
    // Behaviour node to check if the eye is currently not idling.
{

    public override NodeState _Evaluate()
    {
        return (bool)GetData("idling") ? NodeState.FAILURE : NodeState.SUCCESS;
    }
}
