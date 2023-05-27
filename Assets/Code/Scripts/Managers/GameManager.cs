using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager> {
  [SerializeField] private GameUI _gameUI;

  public UnityEvent OnSetupComplete { get; private set; }

  public void LoadMainMenu() {
    PauseManager.Instance.SetIsPausable(false);
    PauseManager.Instance.UnpauseGame();
    SceneNavigationManager.Instance.LoadMainMenuScene();
  }

  protected override void Awake() {
    base.Awake();
    this.OnSetupComplete = new UnityEvent();
  }

  private void Start() {
    // Setup
    this._gameUI.Initialize(this);
    PauseManager.Instance.SetIsPausable(true);

    this.OnSetupComplete?.Invoke();
  }
}
