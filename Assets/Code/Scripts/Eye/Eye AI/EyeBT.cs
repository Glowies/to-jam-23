using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using BehaviourTree;
using Controls;
using UnityEngine;
using UnityEngine.Events;

public class EyeBT : BehaviourTree.Tree
    // Behaviour Tree of the eye enemy AI.
{
    [Header("Debug")]
    public bool debug = false;

    [Header("Interacting Systems")]
    public EyeSight eyeSight;
    public PlayerController player;
    public RoomManager roomManager;
    public Animator animController;
    public Light eyeSelfLight;
    public Light eyeWindowLight;

    [Header("Light Colors")]
    public Color eyeAgitatedColor = new Color(1, 0.34f, 0.34f, 1);
    public Color eyeNonIdlingColor = new Color(0.5f, 0.5f, 0.5f, 1);
    public Color eyeIdleColor = new Color(0.5f, 0.5f, 0.5f, 1);

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
    public int guaranteedAmountOfLooksAfterSwitchingToSearching = 3;

    [Header("Eye Positioning")]
    public float eyeWindowZOffset = 7f;
    public float eyeRoomZOffset = 10f;
    public float eyeWindowAttackingZOffset = 3f;

    [Header("Events")]
    public UnityEvent onStartAttack;
    public UnityEvent onEndAttack;
    public UnityEvent onEndAgitated;
    public UnityEvent onStartSearching;
    public UnityEvent onStartIdle;


    protected override BehaviourNode SetupTree()
    {
        BehaviourNode searching = new Selector(new List<BehaviourNode>
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
                    new CheckPlayerInView(eyeSight, transform, onStartAttack, animController),
                    new Attack(eyeSight, player)
                }),
                // idling while looking thru window
                new LookThroughWindow(transform, animController, onEndAttack),
                // switch windows
                new SwitchWindows(transform, roomManager, onEndAgitated, onStartIdle)
            })
        });


        BehaviourNode root = new Selector(new List<BehaviourNode>
        {
            new Sequence(new List<BehaviourNode>
            {
                new IsNotIdling(),
                searching
            }),

            new Idle(transform, eyeSelfLight, onStartSearching, guaranteedAmountOfLooksAfterSwitchingToSearching)
            

        });

        // setup debug logs
        if (debug)
        {
            onStartAttack.AddListener(() => { Debug.Log("Attack start event"); });
            onEndAttack.AddListener(() => { Debug.Log("Attack end event"); });
            onEndAgitated.AddListener(() => { Debug.Log("Agitated end event"); });
            onStartSearching.AddListener(() => { Debug.Log("Searching start event"); });
            onStartIdle.AddListener(() => { Debug.Log("Idle start event"); });
        }
        

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
        root.SetData("guaranteedLooks", 0);


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
