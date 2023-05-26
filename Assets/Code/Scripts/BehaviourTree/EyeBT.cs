using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using Controls;

public class EyeBT : Tree
    // Behaviour Tree of the eye enemy AI.
{
    public UnityEngine.Transform[] lookingPoints;
    public EyeSight eyeSight;
    public PlayerController player;

    public float idleWaitTime = 5f;
    public float attackStartTime = 0.5f;
    public float damagePerSecond = 0.2f;
    public float decreaseMaxHRPerSecond = 0.1f;

    protected override BehaviourNode SetupTree()
    {
        BehaviourNode root = new Selector(new List<BehaviourNode>
        {
            // switch rooms
            new Sequence(new List<BehaviourNode>
            {
                new CheckPlayerInNextRoom(),
                new SwitchRooms()
            }),
            
            // main behavior
            new Selector (new List<BehaviourNode>
            { 
                // attack
                new Sequence (new List<BehaviourNode>
                {
                    new CheckPlayerInView(eyeSight),
                    new Attack(eyeSight, player)
                }),
                // idling while looking thru window
                new LookThroughWindow(),
                // switch windows
                new SwitchWindows()
            }),
            
            // idle
            new Idle()
        });

        // set values
        root.SetData("idleWaitTime", idleWaitTime);
        root.SetData("attackStartTime", attackStartTime);
        root.SetData("damagePerSecond", damagePerSecond);
        root.SetData("decreaseMaxHRPerSecond", decreaseMaxHRPerSecond);

        return root;
    }
}
