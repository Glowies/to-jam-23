using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using DG.Tweening;

public class Idle : BehaviourNode
    // Behaviour node for when the eye is idling.
{
    private float _waitCounter = 0;
    private bool _switching = true;
    private Transform _eyeTransform;

    public Idle(Transform transform)
    {
        _eyeTransform = transform;
    }



    public override NodeState _Evaluate()
    {
        // might add an idle animation here?

        // switch position
        if (_switching)
        {
            Debug.Log("Idle");
            _switching = false;
            Room currRoom = (Room)GetData("currentRoom");
            float eyeRoomZOffset = (float)GetData("eyeRoomZOffset");
            float retreatTime = (float)GetData("retreatTime");
            _eyeTransform.DOMove(currRoom.transform.position + new Vector3(0, 2, eyeRoomZOffset), retreatTime);
        }

        // get idle wait time from parent data
        if (_waitCounter < (float) GetData("idleWaitTime"))
        {
            // increment counter
            _waitCounter += Time.deltaTime;
            // Debug.Log(_waitCounter + " out of " + (float)GetData("idleWaitTime"));
            parent.SetData("idling", true);
            return NodeState.RUNNING;
        }

        // end with success
        // Debug.Log("Not Idle");
        _waitCounter = 0;
        _switching = true;
        parent.SetData("idling", false);
        return NodeState.SUCCESS;
    }
}
