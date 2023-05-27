using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using BehaviourTree;
using Controls;
using UnityEngine;

public class EyeBT : BehaviourTree.Tree
    // Behaviour Tree of the eye enemy AI.
{

    [Header("Interacting Systems")]
    public EyeSight eyeSight;
    public PlayerController player;
    public RoomManager roomManager;

    [Header("Transition Timings")]
    public float idleWaitTime = 5f;
    public float attackStartTime = 0.01f;
    public float attackLingerTime = 1f;
    public float idleOnWindowWaitTime = 1f;
    public float windowSwitchTime = 0.5f;
    public float roomSwitchTime = 1f;

    [Header("Damage")]
    public float decreaseMaxHRPerSecond = 0.1f;

    [Header("Eye Positioning")]
    public float eyeWindowZOffset = 7f;
    public float eyeRoomZOffset = 10f;
    public float eyeWindowAttackingZOffset = 5f;
    

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
        root.SetData("decreaseMaxHRPerSecond", decreaseMaxHRPerSecond);
        root.SetData("currentRoom", roomManager.GetStartRoom());
        root.SetData("eyeWindowZOffset", eyeWindowZOffset);
        root.SetData("eyeRoomZOffset", eyeRoomZOffset);
        root.SetData("eyeWindowAttackingZOffset", eyeWindowAttackingZOffset);
        root.SetData("attackLingerTime", attackLingerTime);


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
