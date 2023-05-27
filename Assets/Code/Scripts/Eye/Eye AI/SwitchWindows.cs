using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using DG.Tweening;
using UnityEngine;

public class SwitchWindows : BehaviourNode
    // Behaviour node for the eye switching the window its focused on currently.
{
    RoomManager _roomManager;
    Transform _eyeTransform;

    public SwitchWindows(Transform eyeTransform, RoomManager roomManager)
    {
        _roomManager = roomManager;
        _eyeTransform = eyeTransform;
    }


    public override NodeState _Evaluate()
    {
        List<Vector3> windows = _roomManager.GetCurrentRoom().Windows;

        int nextWindowIndex = UnityEngine.Random.Range(0, windows.Count);
        parent.parent.SetData("currWindowIndex", nextWindowIndex);

        UnityEngine.Debug.Log("Switching to window index " + nextWindowIndex);

        // Animation for going to the window set here!

        float eyeZOffset = (float)GetData("eyeWindowZOffset");
        _eyeTransform.DOMove(windows[nextWindowIndex] + new Vector3(0, 0, eyeZOffset), 0.5f);


        return NodeState.SUCCESS;
    }
}
