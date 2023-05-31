using System;
using UnityEngine;
using UnityEngine.Events;
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
        private static WalkingState _walking;
        private static RunningState _running;
        private static HidingState _hiding;
        private static DyingState _dying;
        private PlayerState _state;
        
        // For movement testing, allow speeds to be set through the editor
        [Header("State speed parameters")]
        [SerializeField] private float _walkingSpeed, _runningSpeed;
        [SerializeField] private float _walkingSpeedBlendDuration, _runningSpeedBlendDuration;
        [SerializeField] private float _walkingHeartRateDecrease, _runningHeartRateIncrease, _hidingHeartRateIncrease;

        // ----------------- HR Gauge ----------------
        // Player HAS-A HeartRate Gauge;
        // TODO: The HRGauge should only be exposed to other classes by the provided method GetHRGauge()
        private HRGauge _heartRate = new HRGauge();

        // ----------------- Events ------------------
        public UnityEvent<float> OnScoreUpdate = new UnityEvent<float>();

        // --------------- Bookkeeping ---------------
        // TODO: If we want to extend the player movement to incorporate a rigidbody, but for now we won't
        private Rigidbody _rBody;
        public Animator _animator;
        private float _startXPos;
        private float animatorSpeedValue = 0;

        private PlayerInputActions _playerInputActions;

        void Awake()
        {
            _walking = new WalkingState(_walkingSpeed, _walkingSpeedBlendDuration, _walkingHeartRateDecrease);
            _running = new RunningState(_runningSpeed, _runningSpeedBlendDuration, _runningHeartRateIncrease);
            _hiding = new HidingState(_hidingHeartRateIncrease);
            _dying = new DyingState();
            
            _state = _walking;
            
            _startXPos = transform.position.x;
            _rBody = GetComponent<Rigidbody>();
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
            _state.Movement(transform, _heartRate, Die);

            // Set animation values
            SetAnimatorValues();

            // Calculate distance score
            CalculateScore();
        }

        private void SetAnimatorValues()
        {
            float switchSpeed = 2f;
            if (_state is WalkingState)
            {
                animatorSpeedValue = Mathf.MoveTowards(animatorSpeedValue, 0.5f, switchSpeed * Time.deltaTime);
            } else if (_state is RunningState)
            {
                animatorSpeedValue = Mathf.MoveTowards(animatorSpeedValue, 1, switchSpeed * Time.deltaTime);
            } else if (_state is HidingState)
            {
                animatorSpeedValue = Mathf.MoveTowards(animatorSpeedValue, 0, switchSpeed * Time.deltaTime);
            }

            _animator.SetFloat("speed", animatorSpeedValue);

            if (_state is DyingState)
            {
                _animator.SetBool("dead", true);
            }
        }

        private void CalculateScore()
        {
            float score = Mathf.Round(transform.position.x - _startXPos);
            OnScoreUpdate?.Invoke(score);
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
        
        // ---------------- Input -----------------
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

            GameManager.Instance.OnGameOver?.Invoke();
        }

        private void OnEnable() {
            // OnEnable called before Start
            // PlayerInputController.Instance and this._playerInputController may be uninitialized
            // when the scene is just started

            // reset animator values
            _animator.SetFloat("speed", 0.5f);
            _animator.SetBool("dead", false);

            if (this._playerInputActions == null)
                return;

            this._playerInputActions.Movement.Run.canceled += Walk;
            this._playerInputActions.Movement.Stop.canceled += Walk;
            this._playerInputActions.Movement.Run.performed += Run;
            this._playerInputActions.Movement.Stop.performed += Stop;
        }

        private void OnDisable() {
            this._playerInputActions.Movement.Run.canceled -= Walk;
            this._playerInputActions.Movement.Stop.canceled -= Walk;
            this._playerInputActions.Movement.Run.performed -= Run;
            this._playerInputActions.Movement.Stop.performed -= Stop;
        }
    }
}
