using Controls;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager> {
  [SerializeField] private GameUI _gameUI;
  [SerializeField] private PlayerController _playerController;

  public UnityEvent OnSetupComplete { get; private set; }
  public UnityEvent OnGameOver { get; private set; }

  public void LoadMainMenu() {
    PauseManager.Instance.UnpauseGame();
    PauseManager.Instance.SetIsPausable(false);
    this._gameUI.FadeInForeground(SceneNavigationManager.Instance.LoadMainMenuScene);
  }

  public void ReloadGameScene() {
    this._gameUI.FadeInForeground(SceneNavigationManager.Instance.LoadGameScene);
  }

  protected override void Awake() {
    base.Awake();
    this.OnSetupComplete = new UnityEvent();
    this.OnGameOver = new UnityEvent();

    this.OnGameOver.AddListener(this.PlayerDied);
  }

  private void Start() {
    // Setup
    this._gameUI.Initialize(this);
    PauseManager.Instance.SetIsPausable(true);

    this.OnSetupComplete?.Invoke();
  }

  private void PlayerDied() {
    PauseManager.Instance.SetIsPausable(false);
    Destroy(this._playerController.gameObject);
  }
}
