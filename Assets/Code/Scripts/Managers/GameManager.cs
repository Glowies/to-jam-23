using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager> {
  [SerializeField] private GameUI _gameUI;

  public UnityEvent OnSetupComplete { get; private set; }

  protected override void Awake() {
    base.Awake();
    this.OnSetupComplete = new UnityEvent();
  }

  private void Start() {
    // Setup
    this._gameUI.Initialize(this);

    this.OnSetupComplete?.Invoke();
  }
}
