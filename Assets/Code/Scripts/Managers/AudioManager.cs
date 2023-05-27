using UnityEngine;

public class AudioManager : Singleton<AudioManager> {

  // --- Music and Ambience--- //
  // Sample:
  // [SerializeField] private AudioSource _ambienceLoopSource;


  // -- SFX -- //
  // Sample:
  // [SerializeField] private AudioSource _gameStartSFXSource;

  public void PlaySound() {
    // call AudioSource.Play() on the AudioSource of interest
  }

  public void StopAllMusic() {
    AudioSource[] sources = this.GetComponentsInChildren<AudioSource>();
    foreach (AudioSource source in sources) {
      source.Stop();
    }
  }

  private void Start() {
    // Set up AudioSources that need to ignore game pause
    // Sample:
    // this._ambienceLoopSource.ignoreListenerPause = true;

    PauseManager.Instance.OnPauseToggled.AddListener(this.OnPauseToggled);
  }

  private void OnPauseToggled(bool isPaused) {
    AudioListener.pause = isPaused;
  }

  // -- Music and Ambience -- //
  // Sample methods:
  // private void PlayAmbienceLoop() { _ambienceLoopSource.Play(); }
  // private void StopAmbienceLoop() { _ambienceLoopSource.Stop(); }

  // -- SFX -- //
  // Samples:
  // private void PlayGameStartSFX() { _gameStartSFXSource.Play(); }
  // private void PlayGameStartCountdownSequenceSFX() { _gameStartCountDownSequenceSFXSource.Play(); }
}
