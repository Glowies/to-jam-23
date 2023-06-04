using System.Collections;
using System.Collections.Generic;



namespace BehaviourTree
    // behaviour tree template adapted from https://www.youtube.com/watch?v=aR6wt5BlE-E
{
    public class Switch : BehaviourNode
    // Node that returns the state of its switchVar'th children's state. Fails if switchVar is invalid.
    {
        private int _switchVar = 0;

        public Switch() : base() { }
        public Switch(List<BehaviourNode> children, int switchVar) : base(children) {
            _switchVar = switchVar;
        }

        public override NodeState _Evaluate()
        {

            if (_switchVar < 0 || _switchVar >= children.Count) return NodeState.FAILURE;

            return children[_switchVar]._Evaluate();
        }
    }
}