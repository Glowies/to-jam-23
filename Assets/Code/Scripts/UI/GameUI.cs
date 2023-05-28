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
  [SerializeField] private GameObject _leaderboardSetterUI;
  [SerializeField] private GameObject _scoreboardUI;
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
  
  public void ShowLeaderboard() {
    this._leaderboardSetterUI.SetActive(false);
    this._scoreboardUI.SetActive(true);
  }
  
  public void ShowGameOver(float distance, int roomsTraversed) {
    this._gameOverUI.SetActive(true);
  }

  private void Awake() {
    this._gameStartedForegroundImage.gameObject.SetActive(true);
  }

  private void Start() {
    PauseManager.Instance.OnPauseToggled.AddListener(this.OnPauseToggled);
  }

  private void OnGameSetupComplete() {
    this._foregroundImage
      .DOFade(0, 0.5f)
      .OnComplete(() => this._gameStartedForegroundImage.gameObject.SetActive(false));
  }

  private void OnPauseToggled(bool isPaused) {
    this._pauseUI.SetActive(isPaused);
  }

  private void OnGameOver()
  {
    float distance = _distanceKeeper.GetDistance();
    int roomsTraversed = _roomCountKeeper.GetRoomCount();

    if (LeaderboardManager.Instance.SetPlacement(distance, roomsTraversed) > 0)
    {
      this._leaderboardSetterUI.SetActive(true);
    }
    else
    {
      ShowGameOver(distance, roomsTraversed);
    }
  }
}
