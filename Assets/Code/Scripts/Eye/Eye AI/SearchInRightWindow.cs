using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using DG.Tweening;


public class SearchInRightWindow : BehaviourNode
{
    Transform _eyeTransform;

    public SearchInRightWindow(Transform transform)
    {
        _eyeTransform = transform;
    }

    public override NodeState _Evaluate()
    {
        Room currRoom = (Room)GetData("currentRoom");
        int currWindowIndex = (int)GetData("currWindowIndex");

        if (currWindowIndex + 1 >= currRoom.Windows.Count)
            return NodeState.FAILURE;
        parent.parent.parent.parent.SetData("currWindowIndex", currWindowIndex + 1);

        float eyeZOffset = (float)GetData("eyeWindowZOffset");
        _eyeTransform.DOMove(currRoom.Windows[currWindowIndex + 1] + new Vector3(0, 0, eyeZOffset), 0.5f);

        return NodeState.SUCCESS;

    }


}
