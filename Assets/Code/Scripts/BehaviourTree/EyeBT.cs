using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

public class EyeBT : Tree
    // Behaviour Tree of the eye enemy AI.
{
    public UnityEngine.Transform[] lookingPoints;
    public EyeSight eyeSight;

    public float idleWaitTime = 5f;

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
                    new Attack(eyeSight)
                }),
                // idling while looking thru window
                new LookThroughWindow(),
                // switch windows
                new SwitchWindows()
            }),
            
            // idle
            new Idle()
        });

        root.SetData("idleWaitTime", idleWaitTime);

        return root;
    }
}
