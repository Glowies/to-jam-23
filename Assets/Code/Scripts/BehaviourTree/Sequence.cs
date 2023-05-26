using System.Collections;
using System.Collections.Generic;


namespace BehaviourTree
    // behaviour tree template adapted from https://www.youtube.com/watch?v=aR6wt5BlE-E
{
    public class Sequence : BehaviourNode
        // Node that returns success when all its immediate children return success, and returns failure otherwise.
        // Acts like an AND gate.
    {
        public Sequence() : base() { }
        public Sequence(List<BehaviourNode> children) : base(children) { }

        public override NodeState _Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (BehaviourNode child in children)
            {
                switch (child._Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }
}

