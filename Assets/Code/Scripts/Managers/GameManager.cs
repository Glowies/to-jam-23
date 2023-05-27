using Controls;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager> {
  [SerializeField] private GameUI _gameUI;
  [SerializeField] private PlayerController _playerController;

  public UnityEvent OnSetupComplete { get; private set; }
  public UnityEvent OnGameOver { get; private set; }

  public void LoadMainMenu() {
    PauseManager.Instance.SetIsPausable(false);
    PauseManager.Instance.UnpauseGame();
    SceneNavigationManager.Instance.LoadMainMenuScene();
  }

  public void ReloadGameScene() {
    SceneNavigationManager.Instance.LoadGameScene();
  }

  protected override void Awake() {
    base.Awake();
    this.OnSetupComplete = new UnityEvent();
    this.OnGameOver = new UnityEvent();
  }

  private void Start() {
    // Setup
    this._gameUI.Initialize(this);
    PauseManager.Instance.SetIsPausable(true);

    this.OnSetupComplete?.Invoke();
  }

  private void Update() {
    // Debug for now until death is triggered
    if (Input.GetKeyDown(KeyCode.Backspace)) {
      PauseManager.Instance.SetIsPausable(false);
      Destroy(this._playerController.gameObject);
      this.OnGameOver?.Invoke();
    }
  }
}
