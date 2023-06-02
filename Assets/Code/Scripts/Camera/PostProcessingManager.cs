using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class PostProcessingManager : MonoBehaviour {
  public static PostProcessingManager Instance { get; private set; }

  [SerializeField] private EyeBT eyeBT;

  // Camera Shake
  private const float cameraShakeMinIntensity = 0f;
  private const float cameraShakeMaxIntensity = 0.5f;

  // Bloom
  private const float bloomMinIntensity = 3f;
  private const float bloomMaxIntensity = 6f;

  // Color Adjustments
  private const float exposureMinIntensity = 0f;
  private const float exposureMaxIntensity = 0.5f;

  // Vignette
  private const float vignetteMinIntensity = 0.2f;
  private const float vignetteMaxIntensity = 0.4f;

  // Film Grain
  private const float filmGrainMinIntensity = 0.2f;
  private const float filmGrainMaxIntensity = 1f;

  private const float attackOnDuration = 0.5f;
  private const float attackOffDuration = 0.5f;

  [SerializeField] private CinemachineVirtualCamera virtualCamera;
  [SerializeField] private Volume volume;
  private CinemachineBasicMultiChannelPerlin perlin;
  private Bloom bloom;
  private ColorAdjustments colorAdjustments;
  private Vignette vignette;
  private FilmGrain filmGrain;

  private bool underAttack;
  private float currentAttackEffectIntensity;
  private float attackOnIntensityPerSecond;
  private float attackOffIntensityPerSecond;

  public float ShakeAmplitude {
    get { return this.perlin.m_AmplitudeGain; }
    set { this.perlin.m_AmplitudeGain = value; }
  }

  public float BloomIntensity {
    get { return this.bloom.intensity.value; }
    set { this.bloom.intensity.value = value; }
  }

  public float ExposureValue {
    get { return this.colorAdjustments.postExposure.value; }
    set { this.colorAdjustments.postExposure.value = value; }
  }

  public float SaturationValue {
    get { return this.colorAdjustments.saturation.value; }
    set { this.colorAdjustments.saturation.value = value; }
  }

  public float VignetteIntensity {
    get { return this.vignette.intensity.value; }
    set { this.vignette.intensity.value = value; }
  }

  public float FilmGrainIntensity {
    get { return this.filmGrain.intensity.value; }
    set { this.filmGrain.intensity.value = value; }
  }

  public void DeathFX(float duration) {
    DOTween.To(x => this.ExposureValue = x, this.ExposureValue, 2f, duration).SetEase(Ease.InCirc);
    DOTween.To(x => this.SaturationValue = x, 0, -50f, duration).SetEase(Ease.InCirc);
    DOTween.To(x => this.FilmGrainIntensity = x, FilmGrainIntensity, 1f, duration).SetEase(Ease.InCirc);
    DOTween.To(x => this.ShakeAmplitude = x, ShakeAmplitude, 1f, duration).SetEase(Ease.InCirc);
  }

  public void ResetFX() {
    this.ShakeAmplitude = cameraShakeMinIntensity;
    this.BloomIntensity = bloomMinIntensity;
    this.ExposureValue = exposureMinIntensity;
    this.ExposureValue = 0f;
    this.VignetteIntensity = vignetteMinIntensity;
    this.FilmGrainIntensity = filmGrainMinIntensity;
  }
  
  private void HandleAttackStateChange(bool isAttacking) {
    this.underAttack = isAttacking;
  }

  private void UpdateAttackEffects() {
    this.ShakeAmplitude = this.currentAttackEffectIntensity * (cameraShakeMaxIntensity - cameraShakeMinIntensity) + cameraShakeMinIntensity;
    this.BloomIntensity = this.currentAttackEffectIntensity * (bloomMaxIntensity - bloomMinIntensity) + bloomMinIntensity;
    this.ExposureValue = this.currentAttackEffectIntensity * (exposureMaxIntensity - exposureMinIntensity) + exposureMinIntensity;
    this.VignetteIntensity = this.currentAttackEffectIntensity * (vignetteMaxIntensity - vignetteMinIntensity) + vignetteMinIntensity;
    this.FilmGrainIntensity = this.currentAttackEffectIntensity * (filmGrainMaxIntensity - filmGrainMinIntensity) + filmGrainMinIntensity;
  }

  private void Awake() {
    Instance = this;
  }

  private void Start() {
    this.perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    if (this.volume.profile.TryGet(out this.bloom)) {
      this.bloom.active = true;
      this.BloomIntensity = bloomMinIntensity;
    }

    if (this.volume.profile.TryGet(out this.colorAdjustments)) {
      this.colorAdjustments.active = true;
      this.ExposureValue = exposureMinIntensity;
    }

    if (this.volume.profile.TryGet(out this.vignette)) {
      this.vignette.active = true;
      this.VignetteIntensity = vignetteMinIntensity;
    }

    if (this.volume.profile.TryGet(out this.filmGrain)) {
      this.filmGrain.active = true;
      this.FilmGrainIntensity = filmGrainMinIntensity;
    }

    this.underAttack = false;
    this.currentAttackEffectIntensity = 0f;
    this.attackOnIntensityPerSecond = 1f / attackOnDuration;
    this.attackOffIntensityPerSecond = 1f / attackOffDuration;

    this.eyeBT.OnAttackStateChange.AddListener(this.HandleAttackStateChange);
  }

  private void Update() {
    if (this.underAttack) {
      if (this.currentAttackEffectIntensity < 1f) {
        this.currentAttackEffectIntensity = Mathf.Min(1f, this.currentAttackEffectIntensity + Time.deltaTime * this.attackOnIntensityPerSecond);
        this.UpdateAttackEffects();
      }
    } else {
      if (this.currentAttackEffectIntensity > 0f) {
        this.currentAttackEffectIntensity = Mathf.Max(0f, this.currentAttackEffectIntensity - Time.deltaTime * this.attackOffIntensityPerSecond);
        this.UpdateAttackEffects();
      }
    }
  }
}
