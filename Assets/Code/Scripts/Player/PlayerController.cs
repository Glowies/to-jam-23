using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls
{
    /// <summary>
    /// Class to handle character controls
    /// TODO: Make note of any music plugins we need here...
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        // -------------- Input System ---------------
        private PlayerInputActions _playerInputActions;
        
        // --------------- Player State --------------
        private static WalkingState _walking = new WalkingState();
        private static RunningState _running = new RunningState();
        private static HidingState _hiding = new HidingState();
        private PlayerState _state;
        
        // --------------- Player Speed --------------
        public float BaseSpeed;
        
        void Awake()
        {
            
        }
    
        // Update is called once per frame
        void Update()
        {
            // Delegate movement behaviour to state classes
            _state.Movement(this, BaseSpeed);
        }

        void Walk(InputAction.CallbackContext obj) 
        {
            _state = _walking;
        }
        
        void Run(InputAction.CallbackContext obj)
        {
            _state = _running;
        }
        
        void Stop(InputAction.CallbackContext obj)
        {
            _state = _hiding;
        }
    
        void OnEnable()
        {
            _playerInputActions.Movement.Walk.performed += Walk;
            _playerInputActions.Movement.Walk.Enable();
            
            _playerInputActions.Movement.Run.performed += Run;
            _playerInputActions.Movement.Run.Enable();
    
            _playerInputActions.Movement.Stop.performed += Stop;
            _playerInputActions.Movement.Stop.Enable();
        }
    
        void OnDisable()
        {
            _playerInputActions.Movement.Walk.performed -= Walk;
            _playerInputActions.Movement.Walk.Enable();
            
            _playerInputActions.Movement.Run.performed -= Run;
            _playerInputActions.Movement.Run.Enable();
    
            _playerInputActions.Movement.Stop.performed -= Stop;
            _playerInputActions.Movement.Stop.Enable();
        }
    }
}

