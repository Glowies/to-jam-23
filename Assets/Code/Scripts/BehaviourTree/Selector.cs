using System.Collections;
using System.Collections.Generic;



namespace BehaviourTree
    // behaviour tree template adapted from https://www.youtube.com/watch?v=aR6wt5BlE-E
{
    public class Selector : BehaviourNode
    // Node that returns success when one of its immediate children return success, or returns failure otherwise.
    // Acts like an OR gate.
    {
        public Selector() : base() { }
        public Selector(List<BehaviourNode> children) : base(children) { }

        public override NodeState _Evaluate()
        {

            foreach (BehaviourNode child in children)
            {
                switch (child._Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}