using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PauseManager: Singleton<PauseManager> {

  public UnityAction<bool> OnPauseToggled;

  private bool _isPausable;
  private bool _isPaused;

  private PlayerInputActions _playerInputActions;

  public void SetIsPausable(bool isPausable) {
    this._isPausable = isPausable;
  }

  public void UnpauseGame() {
    Time.timeScale = 1f;
    this._isPaused = false;
    this.OnPauseToggled?.Invoke(this._isPaused);
  }

  private void Start() {
    this._playerInputActions = PlayerInputController.Instance.PlayerInputActions;
    this.OnEnable();
  }

  private void OnEnable() {
    // OnEnable called before Start
    // PlayerInputController.Instance and this._playerInputController may be uninitialized
    // when the scene is just started
    if (this._playerInputActions == null)
      return;

    this._playerInputActions.UI.Pause.performed += this.TogglePausedState;
  }

  private void OnDisable() {
    this._playerInputActions.UI.Pause.performed -= this.TogglePausedState;
  }

  private void TogglePausedState(InputAction.CallbackContext ctx) {
    if (!this._isPausable) {
      return;
    }

    if (this._isPaused) {
      this.UnpauseGame();
    } else {
      this.PauseGame();
    }
  }

  private void PauseGame() {
    print("Pausing");
    Time.timeScale = 0f;
    this._isPaused = true;
    this.OnPauseToggled?.Invoke(this._isPaused);
  }
}
