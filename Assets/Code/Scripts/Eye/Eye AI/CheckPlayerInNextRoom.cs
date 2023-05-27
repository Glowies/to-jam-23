using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

public class CheckPlayerInNextRoom : BehaviourNode
    // Behaviour node for checking if the player is in the next room.
{
    private RoomManager roomManager;

    public CheckPlayerInNextRoom(RoomManager roomManager)
    {
        this.roomManager = roomManager;
    }

    public override NodeState _Evaluate()
    {
        if (roomManager.GetCurrentRoom() != (Room) GetData("currentRoom"))
            return NodeState.SUCCESS;
        return NodeState.FAILURE;
    }
}
