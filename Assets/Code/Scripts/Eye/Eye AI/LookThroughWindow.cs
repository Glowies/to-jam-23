using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using DG.Tweening;
using UnityEngine;

public class LookThroughWindow : BehaviourNode
    // Behaviour node for when the eye is looking through a window.
{
    private float _waitCounter = 0;
    private Transform _eyeTransform;
    private RoomManager _roomManager;

    public LookThroughWindow(Transform transform)
    {
        _eyeTransform = transform;
    }

    public override NodeState _Evaluate()
    {
        UnityEngine.Debug.Log("Idling through a window...");

        // if not looking through a window, switch to first window in window list
        if (GetData("currWindowIndex") == null)
        {
            parent.parent.SetData("currWindowIndex", 0);

            Room currRoom = (Room)GetData("currentRoom");
            float eyeZOffset = (float)GetData("eyeWindowZOffset");
            _eyeTransform.DOMove(currRoom.Windows[0] + new Vector3(0, 0, eyeZOffset), (float)GetData("windowSwitchTime"));

        }

        _waitCounter += UnityEngine.Time.deltaTime;

        if (_waitCounter > (float)GetData("idleOnWindowWaitTime"))
        {
            _waitCounter = 0;
            return NodeState.FAILURE;
        }

        return NodeState.RUNNING;
    }
}
