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
    public Animator animController;

    [Header("Transition Timings")]
    public float idleWaitTime = 5f;
    public float attackStartTime = 0.5f;
    public float attackLingerTime = 2f;
    public float idleOnWindowWaitTime = 1f;
    public float windowSwitchTime = 0.5f;
    public float roomSwitchTime = 1f;
    public float retreatTime = 1.5f;

    [Header("Damage")]
    public float decreaseMaxHRPerSecond = 0.1f;

    [Header("Attitude")]
    [Range(0, 1)] public float aggro = 0.3f;

    [Header("Eye Positioning")]
    public float eyeWindowZOffset = 7f;
    public float eyeRoomZOffset = 10f;
    public float eyeWindowAttackingZOffset = 3f;
    

    protected override BehaviourNode SetupTree()
    {
        BehaviourNode nonIdlingBehaviour = new Selector(new List<BehaviourNode>
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
                    new Attack(eyeSight, player, animController, transform)
                }),
                // idling while looking thru window
                new LookThroughWindow(transform, animController),
                // switch windows
                new SwitchWindows(transform, roomManager)
            })
        });


        BehaviourNode root = new Selector(new List<BehaviourNode>
        {
            new Sequence(new List<BehaviourNode>
            {
                new IsNotIdling(),
                nonIdlingBehaviour
            }),

            new Idle(transform)
            

        });

        // set values
        root.SetData("idleWaitTime", idleWaitTime);
        root.SetData("idling", true);
        root.SetData("attackStartTime", attackStartTime);
        root.SetData("idleOnWindowWaitTime", idleOnWindowWaitTime);
        root.SetData("windowSwitchTime", windowSwitchTime);
        root.SetData("roomSwitchTime", roomSwitchTime);
        root.SetData("retreatTime", retreatTime);
        root.SetData("decreaseMaxHRPerSecond", decreaseMaxHRPerSecond);
        root.SetData("aggro", aggro);
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
