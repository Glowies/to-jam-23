using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using BehaviourTree;
using UnityEngine;

public class CheckPlayerInView : BehaviourNode
    // Node for checking if the player is in the view space of the eye. If an attack is ongoing, this will return success. If the player moves away from view during an attack state, the attack will be ended.
{
    private EyeSight _eyeSight;
    private float _waitCounter = 0;
    private bool _waiting = true;
    private float _lingerCounter = 0;
    private UnityEngine.Events.UnityEvent _onStartAttack;
    private Animator _animController;
    private Transform _eyeTransform;

    public CheckPlayerInView(EyeSight eyeSight, Transform eyeTransform, UnityEngine.Events.UnityEvent onStartAttack, Animator animController)
    {
        _eyeSight = eyeSight;
        _eyeTransform = eyeTransform;
        _onStartAttack = onStartAttack;
        _animController = animController;
    }

    public override NodeState _Evaluate()
    {
        if (_eyeSight.IsTargetInSight())
        {
            if (_waiting)
            {
                _waitCounter += UnityEngine.Time.deltaTime;

                // if the player's been visible to the eye for a certain amount of time, start attack
                if (_waitCounter < (float)GetData("attackStartTime"))
                    return NodeState.RUNNING;
                else
                {
                    // UnityEngine.Debug.Log("Player detected!");

                    // start attack

                    // get closer to window
                    Room currRoom = (Room)GetData("currentRoom");

                    if (GetData("currWindowIndex") == null)
                    {
                        // UnityEngine.Debug.LogError("Could not find window to attack through");
                        return NodeState.FAILURE;
                    }
                    int currWindowIndex = (int)GetData("currWindowIndex");

                    float eyeZOffset = (float)GetData("eyeWindowAttackingZOffset");
                    _eyeTransform.DOMove(currRoom.Windows[currWindowIndex] + new Vector3(0, 0, eyeZOffset), (float)GetData("windowSwitchTime"));
                    _animController.SetBool("isAttacking", true);
                    _waitCounter = 0;
                    _waiting = false;
                    _onStartAttack.Invoke();
                    return NodeState.SUCCESS;
                }
            } else
            {
                return NodeState.SUCCESS;
            }

        } else
        {
            // UnityEngine.Debug.Log("Player not detected");

            // linger a bit after player moves out of view
            if (!_waiting)
            {
                // UnityEngine.Debug.Log("Lingering");
                float _attackLingerTime = (float)GetData("attackLingerTime");
                _lingerCounter += UnityEngine.Time.deltaTime;

                if (_lingerCounter < _attackLingerTime)
                    return NodeState.SUCCESS;
            }

            // UnityEngine.Debug.Log("Not lingering");
            _lingerCounter = 0;
            _waitCounter = 0;
            _waiting = true;
            return NodeState.FAILURE;
        }


    }
}
