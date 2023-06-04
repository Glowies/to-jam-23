using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using DG.Tweening;

public class IsNotIdling : BehaviourNode
    // Behaviour node to check if the eye is currently not idling.
{
    private readonly Light _selfLight;
    private readonly Light _windowLight;
    
    public IsNotIdling(Light selfLight, Light windowLight)
    {
        _selfLight = selfLight;
        _windowLight = windowLight;
    }

    public override NodeState _Evaluate()
    {
        var isIdling = (bool)GetData("idling");

        if (isIdling)
        {
            return NodeState.FAILURE;
        }
        
        _windowLight.intensity = (float) GetData("eyeNonIdlingLightIntensity");
        _windowLight.color = (Color) GetData("eyeNonIdlingColor");
        
        return NodeState.SUCCESS;
    }
}
