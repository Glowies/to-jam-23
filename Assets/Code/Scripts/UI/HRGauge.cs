using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI 
{
    [RequireComponent(typeof(Slider))]
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
        [SerializeField] private GameObject _gaugeScaler;
        [SerializeField] private GameObject _heartImage;
        [SerializeField] private GameObject _pulseImage;
        [SerializeField] private GameObject _pulseImage2;
        private Controls.HRGauge _heartImageRateController;
        
        private State _state;

        private Vector3 _heartImageStartScale;
        private readonly WaitForSeconds _waitForHeartBeat = new WaitForSeconds(0.1f);
        private readonly WaitForSeconds _waitForHeavyPace = new WaitForSeconds(0.3f);
        private readonly WaitForSeconds _waitForNormalPace = new WaitForSeconds(0.8f);

        private void Start()
        {
            _heartImageStartScale = _heartImage.transform.localScale;
            _state = State.NormalBeating;
            StartCoroutine(NormalPulse());
            
            // Expensive, but this will only be called at the start
            _heartImageRateController = FindObjectOfType<Controls.PlayerController>().GetHRGauge();

            _heartImageRateController.OnHRIncrease.AddListener(OnHRIncrease);
            _heartImageRateController.OnHRDecrease.AddListener(OnHRDecrease);
            _heartImageRateController.OnMaxHRDecrease.AddListener(OnMaxHRDecrease);
        }

        private void OnHRIncrease(float value)
        {
            GetComponent<Slider>().DOValue(value, 0.1f);
            
            // Heart pulsates furiously!
            if (_state == State.HeavyBeating) return;
            
            _state = State.HeavyBeating;
            StopAllCoroutines();
            StartCoroutine(HeavyPulse());

            // In addition to setting the slider value, what else should be done? 
        }

        private void OnHRDecrease(float value)
        {
            GetComponent<Slider>().DOValue(value, 0.1f);
            
            if (_state == State.NormalBeating) return;
            
            // Heart pulsates normally
            _state = State.NormalBeating;
            StopAllCoroutines();
            StartCoroutine(NormalPulse());
        }
        
        private void OnMaxHRDecrease(float value)
        {
            // Chisel down the max width of the bar
            Vector3 targetScale = new Vector3(value / 100, 1, 1);
            _gaugeScaler.transform.DOScale(targetScale, 0.1f);
            
            // _state = State.EyeAttacking;
        }
        
        private IEnumerator HeavyPulse()
        {
            while (_state == State.HeavyBeating)
            {
                _heartImage.transform.localScale = _heartImageStartScale;
                
                // TODO: Match timing with the SFX of the heart beating later
                _heartImage.transform.DOScale(_heartImageStartScale * 1.1f, 0.08f);

                yield return this._waitForHeartBeat;
                
                // Scale heart back
                _heartImage.transform.DOScale(_heartImageStartScale, 0.08f);

                yield return this._waitForHeavyPace;
            }
        }
        
        private IEnumerator NormalPulse()
        {
            while (_state == State.NormalBeating)
            {
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

