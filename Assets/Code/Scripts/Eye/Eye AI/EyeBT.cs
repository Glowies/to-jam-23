using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using BehaviourTree;
using Controls;

public class EyeBT : Tree
    // Behaviour Tree of the eye enemy AI.
{
    public EyeSight eyeSight;
    public PlayerController player;
    public RoomManager roomManager;

    public float idleWaitTime = 5f;
    public float attackStartTime = 0.01f;
    public float idleOnWindowWaitTime = 1f;
    public float windowSwitchTime = 0.5f;
    public float roomSwitchTime = 1f;
    public float damagePerSecond = 0.2f;
    public float eyeWindowZOffset = 7f;
    public float eyeRoomZOffset = 10f;
    public float eyeWindowAttackingZOffset = 5f;
    public float decreaseMaxHRPerSecond = 0.1f;

    protected override BehaviourNode SetupTree()
    {
        BehaviourNode root = new Selector(new List<BehaviourNode>
        {
            // switch rooms
            new Sequence(new List<BehaviourNode>
            {
                new CheckPlayerInNextRoom(roomManager),
                new SwitchRooms(roomManager, transform)
            }),
            
            // main behavior
            new Selector (new List<BehaviourNode>
            { 
                // attack
                new Sequence (new List<BehaviourNode>
                {
                    new CheckPlayerInView(eyeSight),
                    new Attack(eyeSight, player, transform)
                }),
                // idling while looking thru window
                new LookThroughWindow(transform),
                // switch windows
                new SwitchWindows(transform, roomManager)
            }),
            
            // idle
            new Idle()
        });

        // set values
        root.SetData("idleWaitTime", idleWaitTime);
        root.SetData("attackStartTime", attackStartTime);
        root.SetData("idleOnWindowWaitTime", idleOnWindowWaitTime);
        root.SetData("windowSwitchTime", windowSwitchTime);
        root.SetData("roomSwitchTime", roomSwitchTime);
        root.SetData("damagePerSecond", damagePerSecond);
        root.SetData("decreaseMaxHRPerSecond", decreaseMaxHRPerSecond);
        root.SetData("currentRoom", roomManager.GetStartRoom());
        root.SetData("eyeWindowZOffset", eyeWindowZOffset);
        root.SetData("eyeRoomZOffset", eyeRoomZOffset);
        root.SetData("eyeWindowAttackingZOffset", eyeWindowAttackingZOffset);


        return root;
    }

    public object getFocusedWindow()
        // get the current window position eye is focused on. return null if no such window exists.
    {
        if (getRootData("currWindowIndex") == null) return null;
        if (getRootData("currentRoom") == null) return null;

        return ((Room)getRootData("currentRoom")).Windows[(int)getRootData("currWindowIndex")];
    }
}
