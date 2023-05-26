using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

public class SwitchWindows : BehaviourNode
    // Behaviour node for the eye switching the window its focused on currently.
{
    List<UnityEngine.Vector3> windows;

    public SwitchWindows()
    {
        windows = ((Room)GetData("currentRoom")).Windows;
    }


    public override NodeState _Evaluate()
    {
        parent.parent.SetData("currWindowIndex", UnityEngine.Random.Range(0, windows.Count));

        // Animation for going to the window set here!

        return NodeState.SUCCESS;
    }
}
