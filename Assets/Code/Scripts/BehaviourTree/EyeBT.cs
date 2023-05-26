using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

public class EyeBT : Tree
{
    public UnityEngine.Transform[] lookingPoints;



    protected override BehaviourNode SetupTree()
    {
        BehaviourNode root = new Sequence(new List<BehaviourNode>
        {
            // switch rooms
            new SwitchRooms(),
            // main behavior sequence
            new Sequence(new List<BehaviourNode>
            {
                // looking
                new Selector (new List<BehaviourNode>
                { 
                    // attack
                    new Sequence (new List<BehaviourNode>
                    {
                        new CheckPlayerInView(),
                        new Attack()
                    }),
                    new LookThroughWindow()
                }),
                // switch windows
                new SwitchWindows()
            }),
            // idle
            new Idle()
        });

        return root;
    }
}
