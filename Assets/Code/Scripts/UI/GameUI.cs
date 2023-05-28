using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
  [SerializeField] private Image _gameStartedForegroundImage;
  [SerializeField] private Image _foregroundImage;
  [SerializeField] private GameObject _pauseUI;
  [SerializeField] private GameObject _gameOverUI;
  [SerializeField] private DistanceUI _distanceKeeper;
  [SerializeField] private RoomCountUI _roomCountKeeper;

  public void Initialize(GameManager gameManager) {
    gameManager.OnSetupComplete.AddListener(this.OnGameSetupComplete);
    gameManager.OnGameOver.AddListener(this.OnGameOver);
  }

  public void FadeInForeground(UnityAction onComplete) {
    this._foregroundImage.gameObject.SetActive(true);
    this._foregroundImage
      .DOFade(1, 0.5f)
      .OnComplete(() => onComplete?.Invoke());
  }

  private void Awake() {
    this._gameStartedForegroundImage.gameObject.SetActive(true);
  }

  private void Start() {
    PauseManager.Instance.OnPauseToggled.AddListener(this.OnPauseToggled);
  }

  private void OnGameSetupComplete() {
    this._gameStartedForegroundImage
      .DOFade(0, 1f)
      .OnComplete(() => this._gameStartedForegroundImage.gameObject.SetActive(false));
  }

  private void OnPauseToggled(bool isPaused) {
    this._pauseUI.SetActive(isPaused);
  }

  private void OnGameOver() {
    this._gameOverUI.SetActive(true);

    // Fetch the room count to display with the distance
    this._gameOverUI.GetComponent<GameOverUI>().SetScore(_distanceKeeper.GetDistance(), _roomCountKeeper.GetRoomCount());
  }
}
