using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CinemachineShake : MonoBehaviour
{
  public static CinemachineShake Instance { get; private set; }

  private const float attackShakeMaxIntensity = 0.8f;
  private const float attackShakeTransitionSpeed = 0.5f;

  [SerializeField] private CinemachineVirtualCamera virtualCamera;
  private CinemachineBasicMultiChannelPerlin perlin;
  private bool underAttack;
  private float currentShakeAmplitude {
    get { return this.perlin.m_AmplitudeGain; }
    set { this.perlin.m_AmplitudeGain = value; }
  }

  public void StartAttackShake() {
    this.underAttack = true;
  }

  public void StopAttackShake() {
    this.underAttack = false;
  }

  private void Awake() {
    Instance = this;
  }

  private void Start() {
    this.perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    this.underAttack = false;
  }

  // Deprecated
  private void DoSmoothCameraShake(float intensity, float transitionTime, TweenCallback callback) {
    DOTween.To(() => this.currentShakeAmplitude, x => this.currentShakeAmplitude = x, intensity, transitionTime).OnComplete(callback);
  }

  // Deprecated
  private void DoCameraShakeImpulse(float intensity, float duration) {
    this.perlin.m_AmplitudeGain = intensity;
    this.DoSmoothCameraShake(0f, duration, null);
  }

  private void Update() {
    // This is a bit of a messy implementation, DOTween was causing issues. May revisit!
    if (this.underAttack) {
      if (this.currentShakeAmplitude < attackShakeMaxIntensity) {
        this.currentShakeAmplitude = Mathf.Min(attackShakeMaxIntensity, this.currentShakeAmplitude + Time.deltaTime * attackShakeTransitionSpeed);
      }
    }
    else {
      if (this.currentShakeAmplitude > 0) {
        this.currentShakeAmplitude = Mathf.Max(0, this.currentShakeAmplitude - Time.deltaTime * attackShakeTransitionSpeed);
      }
    }
  }
}
