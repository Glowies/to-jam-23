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
        private static DyingState _dying = new DyingState();
        private PlayerState _state;
        
        // ----------------- HR Gauge ----------------
        // Player HAS-A HeartRate Gauge;
        // TODO: The HRGauge should only be exposed to other classes by the provided method GetHRGauge()
        private HRGauge _heartRate = new HRGauge();
        
        // --------------- Bookkeeping ---------------
        // TODO: If we want to extend the player movement to incorporate a rigidbody, but for now we won't
        private Rigidbody _rBody;
        public float BaseSpeed;
        
        void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _rBody = GetComponent<Rigidbody>();
            
            _state = _walking;
        }
    
        // Update is called once per frame
        void Update()
        {
            // Delegate movement behaviour to state classes
            _state.Movement(this, _rBody, BaseSpeed);
        }

        void Walk(InputAction.CallbackContext obj) 
        {
            // Cache previous state and call OnExit and OnEnter
            var prevState = _state;
            _state.OnExit(_walking);
            _state = _walking;
            _state.OnEnter(prevState);
        }
        
        void Run(InputAction.CallbackContext obj)
        {
            var prevState = _state;
            _state.OnExit(_running);
            _state = _running;
            _state.OnEnter(prevState);
        }
        
        void Stop(InputAction.CallbackContext obj)
        {
            var prevState = _state;
            _state.OnExit(_hiding);
            _state = _hiding;
            _state.OnEnter(prevState);
        }

        public void Die()
        {
            var prevState = _state;
            _state.OnExit(_dying);
            _state = _dying;
            _state.OnEnter(prevState);
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
            _playerInputActions.Movement.Walk.Disable();
            
            _playerInputActions.Movement.Run.performed -= Run;
            _playerInputActions.Movement.Run.Disable();
    
            _playerInputActions.Movement.Stop.performed -= Stop;
            _playerInputActions.Movement.Stop.Disable();
        }
    }
}

