using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using DG.Tweening;

public class SearchInLeftWindow : BehaviourNode
{
    Transform _eyeTransform;

    public SearchInLeftWindow(Transform transform)
    {
        _eyeTransform = transform;
    }

    public override NodeState _Evaluate()
    {
        Room currRoom = (Room)GetData("currentRoom");
        int currWindowIndex = (int)GetData("currWindowIndex");

        if (currWindowIndex == 0)
            return NodeState.FAILURE;
        parent.parent.parent.parent.SetData("currWindowIndex", currWindowIndex - 1);

        float eyeZOffset = (float)GetData("eyeWindowZOffset");
        _eyeTransform.DOMove(currRoom.Windows[currWindowIndex - 1] + new Vector3(0, 0, eyeZOffset), 0.5f);

        return NodeState.SUCCESS;
        
    }
}
