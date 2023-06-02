using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using DG.Tweening;

public class SwitchRooms : BehaviourNode
    // Room switching behaviour for the eye.
    // Move the eye to the next room and update data on the root about which room the eye is in.
{

    RoomManager _roomManager;
    Transform _eyeTransform;

    public SwitchRooms(RoomManager roomManager, Transform transform)
    {
        _roomManager = roomManager;
        _eyeTransform = transform;
    }

    public override NodeState _Evaluate()
    {
        // Debug.Log("Switching to next room");

        Room nextRoom = _roomManager.GetCurrentRoom();
        parent.parent.parent.parent.SetData("currentRoom", nextRoom);
        parent.parent.parent.parent.SetData("currWindowIndex", 0); // reset window index

        float eyeZOffset = (float)GetData("eyeRoomZOffset");
        _eyeTransform.DOMove(nextRoom.transform.position + new Vector3(0, 0, eyeZOffset), 0.5f);

        return NodeState.SUCCESS;
    }
}
