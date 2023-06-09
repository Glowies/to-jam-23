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
    private Light _selfLight;
    private Light _windowLight;
    private UnityEngine.Events.UnityEvent _onStartSearching;
    private int _guaranteedLooks;

    public Idle(Transform eyeTransform, Light selfLight, Light windowLight, UnityEngine.Events.UnityEvent onStartSearching, int guaranteedLooks)
    {
        _eyeTransform = eyeTransform;
        _selfLight = selfLight;
        _windowLight = windowLight;
        _onStartSearching = onStartSearching;
        _guaranteedLooks = guaranteedLooks;
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
            _selfLight.DOIntensity(0, retreatTime);
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

        // end idling
        // Debug.Log("Not Idle");
        _waitCounter = 0;
        _switching = true;
        _selfLight.intensity = 12.34f;
        _windowLight.intensity = (float) GetData("eyeIdleLightIntensity");
        _windowLight.color = (Color) GetData("eyeIdleColor");
        parent.SetData("idling", false);

        // look at least 3 times before available to idle again
        parent.SetData("guaranteedLooks", _guaranteedLooks);
        _onStartSearching.Invoke();
        return NodeState.SUCCESS;
    }
}
