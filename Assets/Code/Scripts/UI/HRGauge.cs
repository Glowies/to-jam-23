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
        // --------------- Bookkeeping ---------------
        private Controls.HRGauge _heartRateController;

        void Start()
        {
            // Expensive, but this will only be called at the start
            _heartRateController = FindObjectOfType<Controls.PlayerController>().GetHRGauge();

            _heartRateController.OnHRIncrease.AddListener(OnHRIncrease);
            _heartRateController.OnHRDecrease.AddListener(OnHRDecrease);
            _heartRateController.OnMaxHRDecrease.AddListener(OnMaxHRDecrease);
        }

        void OnHRIncrease(float value)
        {
            GetComponent<Slider>().DOValue(value, 0.1f);
            
            // In addition to setting the slider value, what else should be done? 
        }
        
        void OnHRDecrease(float value)
        {
            GetComponent<Slider>().DOValue(value, 0.1f);
        }
        
        void OnMaxHRDecrease(float value)
        {
            
        }
    }
}

