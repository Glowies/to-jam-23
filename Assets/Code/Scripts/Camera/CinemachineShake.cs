using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
  public static CinemachineShake Instance { get; private set; }

  [SerializeField] private CinemachineBasicMultiChannelPerlin perlin;

  private float shakeTimer;
  private float shakeTimerTotal;
  private float startingIntensity;

  private void Awake() {
    Instance = this;
  }

  public void ShakeCamera(float intensity, float time) {
    perlin.m_AmplitudeGain = intensity;

    startingIntensity = intensity;
    shakeTimerTotal = time;
    shakeTimer = time;
  }

  private void Update() {
    if (shakeTimer > 0f) {
      shakeTimer -= Time.deltaTime;
      perlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, shakeTimer / shakeTimerTotal);
    }
  }
}
