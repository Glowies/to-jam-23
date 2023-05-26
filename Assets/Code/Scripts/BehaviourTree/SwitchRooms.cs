using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

public class SwitchRooms : BehaviourNode
    // Room switching behaviour for the eye.
    // Move the eye to the next room and update data on the root about which room the eye is in.
{

    RoomManager _roomManager;

    public SwitchRooms(RoomManager roomManager)
    {
        _roomManager = roomManager;
    }

    public override NodeState _Evaluate()
    {

        parent.parent.SetData("currRoom", _roomManager.GetCurrentRoom());
        return NodeState.SUCCESS;
    }
}
