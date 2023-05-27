using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CinemachineShake : MonoBehaviour
{
  public static CinemachineShake Instance { get; private set; }

  [SerializeField] private CinemachineVirtualCamera virtualCamera;
  private CinemachineBasicMultiChannelPerlin perlin;

  private bool on;

  private void Awake() {
    Instance = this;
  }

  private void Start() {
    this.perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
  }

  public void DoSmoothCameraShake(float intensity, float transitionTime) {
    DOTween.To(() => this.perlin.m_AmplitudeGain, x => this.perlin.m_AmplitudeGain = x, intensity, transitionTime);
  }

  private void Update() {
    if (Input.GetKeyDown(UnityEngine.KeyCode.H)) {
      this.on = !this.on;
      if (this.on) {
        this.DoSmoothCameraShake(5, 1);
      } else {
        this.DoSmoothCameraShake(0, 1);
      }
    }
  }
}
