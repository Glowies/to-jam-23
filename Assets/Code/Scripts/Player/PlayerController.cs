using System;
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

        private PlayerInputActions _playerInputActions;

        void Awake()
        {
            _rBody = GetComponent<Rigidbody>();
            _state = _walking;
        }

        void Start() {
            // Need this due to race condition during scene Awake->OnEnable calls
            this._playerInputActions = PlayerInputController.Instance.PlayerInputActions;
            OnEnable();
        }

        // Update is called once per frame
        void Update()
        {
            // Delegate movement behaviour to state classes
            _state.Movement(transform, _heartRate, Die, BaseSpeed);
        }

        // --------------- Getters ---------------
        public HRGauge GetHRGauge()
        {
            return _heartRate;
        }

        public PlayerState GetPlayerState()
        {
            return _state;
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
            
            // _playerInputActions.Movement.Walk.performed -= Walk;
            // _playerInputActions.Movement.Run.performed -= Run;
            // _playerInputActions.Movement.Stop.performed -= Stop;

            GameManager.Instance.OnGameOver?.Invoke();
        }

        private void OnEnable() {
            // OnEnable called before Start
            // PlayerInputController.Instance and this._playerInputController may be uninitialized
            // when the scene is just started
            if (this._playerInputActions == null)
                return;

            this._playerInputActions.Movement.Walk.performed += Walk;
            this._playerInputActions.Movement.Run.performed += Run;
            this._playerInputActions.Movement.Stop.performed += Stop;
        }

        private void OnDisable() {
            this._playerInputActions.Movement.Walk.performed -= Walk;
            this._playerInputActions.Movement.Run.performed -= Run;
            this._playerInputActions.Movement.Stop.performed -= Stop;
        }
    }
}
