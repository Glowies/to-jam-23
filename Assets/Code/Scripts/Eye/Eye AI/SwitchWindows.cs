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

        if (GetData("agitated") == null)
        {
            parent.SetData("agitated", false);
        }

        bool agitated = (bool)GetData("agitated");
        int currWindowIndex = (int)GetData("currWindowIndex");

        int nextWindowIndex = (agitated && currWindowIndex + 1 < windows.Count) ? currWindowIndex + 1 : UnityEngine.Random.Range(0, windows.Count);
        

        parent.parent.SetData("currWindowIndex", nextWindowIndex);

        // UnityEngine.Debug.Log("Switching to window index " + nextWindowIndex);

        // Animation for going to the window set here!

        float eyeZOffset = (agitated) ? (float)GetData("eyeWindowAttackingZOffset") : (float)GetData("eyeWindowZOffset");
        _eyeTransform.DOMove(windows[nextWindowIndex] + new Vector3(0, 0, eyeZOffset), 0.5f);

        parent.SetData("agitated", false);


        return NodeState.SUCCESS;
    }
}
