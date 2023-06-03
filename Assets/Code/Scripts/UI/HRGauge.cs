using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace UI 
{
    public class HRGauge : MonoBehaviour
    {
        private enum State {
            // The HeavyBeating state is activated when the player is running or hiding, indicating
            // that the player bpm is rising and the heart is pulsing furiously
            HeavyBeating,
            // The NormalBeating state is activated when the player is walking, which causes the heart
            // to pulsate a normal amount
            NormalBeating,
            // TODO: Not sure what the eye attacking state should do yet as far as the heart is concerned...
            EyeAttacking
        }
        
        // --------------- Bookkeeping ---------------
        [Header("Music Events")]
        // Music events kept in the HRGauge UI class to match beating animations
        public UnityEvent onHeavyBeating;
        public UnityEvent onNormalBeating;

        [Header("UI")]
        [SerializeField] private GameObject _heartImage;
        [SerializeField] private Image _pulseImage;
        [SerializeField] private Image _pulseImage2;
        [SerializeField] private Image _heartGauge;
        private Controls.HRGauge _heartImageRateController;
        
        private State _state;

        private Vector3 _heartImageStartScale, _pulseImageStartScale;
        private Color _pulseImageStartColor;
        private readonly WaitForSeconds _waitForHeartBeat = new WaitForSeconds(0.1f);
        private readonly WaitForSeconds _waitForHeavyPace = new WaitForSeconds(0.3f);
        private readonly WaitForSeconds _waitForNormalPace = new WaitForSeconds(0.8f);

        private void Start()
        {
            _heartImageStartScale = _heartImage.transform.localScale;
            _pulseImageStartScale = _pulseImage.transform.localScale;
            
            Color color = _pulseImage.color;
            _pulseImageStartColor = new Color(color.r, color.g, color.b, 0.03f);
            
            _state = State.NormalBeating;
            StartCoroutine(NormalPulse());
            
            // Set heartbeat to starting amount
            _heartGauge.material.SetFloat("_HeartFill", 100f);
            _heartGauge.material.SetFloat("_CorruptionFill", 0.3f);
            
            // Expensive, but this will only be called at the start
            _heartImageRateController = FindObjectOfType<Controls.PlayerController>().GetHRGauge();

            _heartImageRateController.OnHRIncrease.AddListener(OnHRIncrease);
            _heartImageRateController.OnHRDecrease.AddListener(OnHRDecrease);
            _heartImageRateController.OnMaxHRDecrease.AddListener(OnMaxHRDecrease);
        }

        private void OnHRIncrease(float value)
        {
            // Drain the colour out of the heart
            _heartGauge.material.SetFloat("_HeartFill", 100f - value);

            // Heart pulsates furiously!
            if (_state == State.HeavyBeating) return;
            
            _state = State.HeavyBeating;
            StopAllCoroutines();
            StartCoroutine(HeavyPulse());

            // In addition to setting the gauge value, what else should be done? 
        }

        private void OnHRDecrease(float value)
        {
            // Replenish the colour of the heart
            _heartGauge.material.SetFloat("_HeartFill", 100f - value);

            // Heart pulsates normally
            if (_state == State.NormalBeating) return;
            
            _state = State.NormalBeating;
            StopAllCoroutines();
            StartCoroutine(NormalPulse());
        }
        
        private void OnMaxHRDecrease(float value)
        {
            // Replenish the colour of the heart
            _heartGauge.material.SetFloat("_CorruptionFill", 100f - value);

            // _state = State.EyeAttacking;
        }
        
        private IEnumerator HeavyPulse()
        {
            while (_state == State.HeavyBeating)
            {
                onHeavyBeating?.Invoke();
                
                _pulseImage.transform.localScale = _pulseImageStartScale;
                _pulseImage2.transform.localScale = _pulseImageStartScale;
                
                _heartImage.transform.localScale = _heartImageStartScale;
                
                // TODO: Match timing with the SFX of the heart beating later
                _heartImage.transform.DOScale(_heartImageStartScale * 1.1f, 0.08f);

                _pulseImage.color = _pulseImageStartColor;
                _pulseImage.transform.DOScale(_pulseImageStartScale * 1.5f, 0.3f);
                _pulseImage.DOFade(0f, 0.2f);

                yield return this._waitForHeartBeat;
                
                _pulseImage2.color = _pulseImageStartColor;
                _pulseImage2.transform.DOScale(_pulseImageStartScale * 1.5f, 0.3f);
                _pulseImage2.DOFade(0f, 0.2f);
                
                // Scale heart back
                _heartImage.transform.DOScale(_heartImageStartScale, 0.08f);

                yield return this._waitForHeavyPace;
            }
        }
        
        private IEnumerator NormalPulse()
        {
            while (_state == State.NormalBeating)
            {
                onNormalBeating?.Invoke();
                
                _heartImage.transform.localScale = _heartImageStartScale;
                
                // TODO: Match timing with the SFX of the heart beating later
                _heartImage.transform.DOScale(_heartImageStartScale * 1.1f, 0.08f);

                yield return this._waitForHeartBeat;
                
                // Scale heart back
                _heartImage.transform.DOScale(_heartImageStartScale, 0.08f);

                yield return this._waitForNormalPace;
            }
        }
    }
}

