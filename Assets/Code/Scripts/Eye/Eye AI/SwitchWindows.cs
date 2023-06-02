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
        // get agitated status
        if (GetData("agitated") == null)
            parent.SetData("agitated", false);
        bool agitated = (bool)GetData("agitated");

        // UnityEngine.Debug.Log("Switching to window index " + nextWindowIndex);
        // Animation for going to the window set here!

        // get next distance from window
        float eyeZOffset = (agitated) ? (float)GetData("eyeWindowAttackingZOffset") : (float)GetData("eyeWindowZOffset");
        
        // reset agitation state
        parent.SetData("agitated", false);


        // retreat depending on agressiveness
        // if started agitated, keep on the windows
        float aggro = (float) GetData("aggro");
        float r = Random.Range(0f, 1f);
        if (!agitated && r >= aggro)
        {
            Debug.Log("Retreat...");
            return NodeState.FAILURE;
        } else
        {
            List<Vector3> windows = _roomManager.GetCurrentRoom().Windows;

            // switch windows in data
            int currWindowIndex = (int)GetData("currWindowIndex");
            int nextWindowIndex = (agitated && currWindowIndex + 1 < windows.Count) ? currWindowIndex + 1 : UnityEngine.Random.Range(0, windows.Count);
            parent.parent.parent.parent.SetData("currWindowIndex", nextWindowIndex);

            // move to next window
            _eyeTransform.DOMove(windows[nextWindowIndex] + new Vector3(0, 0, eyeZOffset), 0.5f);
            return NodeState.SUCCESS;
        }
    }
}
