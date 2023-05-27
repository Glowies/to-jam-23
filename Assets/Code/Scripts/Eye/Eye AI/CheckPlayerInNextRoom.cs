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
        UnityEngine.Debug.Log("Checking if player is in next room...");
        if (roomManager.GetCurrentRoom() != (Room) GetData("currentRoom"))
        {
            UnityEngine.Debug.Log("Player is in next room");
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
