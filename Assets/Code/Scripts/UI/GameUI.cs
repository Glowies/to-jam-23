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
  [SerializeField] private PostProcessingManager _postProcessingManager;
  [SerializeField] private ControlsUI _controlsUI;

  public void Initialize(GameManager gameManager) {
    gameManager.OnSetupComplete.AddListener(this.OnGameSetupComplete);
    gameManager.OnGameOver.AddListener(this.OnGameOver);
    this._controlsUI.Initialize(gameManager.PlayerController);
  }

  public void FadeInForeground(UnityAction onComplete) {
    this._foregroundImage.gameObject.SetActive(true);
    this._foregroundImage
      .DOFade(1, 0.5f)
      .OnComplete(() => onComplete?.Invoke());
  }
  private void HideLeaderboardSelection() {
    this._leaderboardSetterUI.SetActive(false);
    ShowLeaderboard();
  }

  public void ShowLeaderboard() {
    this._scoreboardUI.SetActive(true);
  }

  public void HideLeaderboard() {
    this._scoreboardUI.SetActive(false);
  }

  public void ShowGameOver() {
    // Get the score from the ScoreManager and set the final score for the game over menu
    var score = ScoreManager.Instance.GetScore();

    this._gameOverUI.SetActive(true);
    this._gameOverUI.GetComponent<GameOverUI>().SetScore(score.Item1, score.Item2);
  }

  private void Awake() {
    this._gameStartedForegroundImage.gameObject.SetActive(true);
  }

  private void Start() {
    PauseManager.Instance.OnPauseToggled.AddListener(this.OnPauseToggled);
    LeaderboardManager.Instance.OnLeaderboardConfirmed.AddListener(HideLeaderboardSelection);
  }

  private void OnGameSetupComplete() {
    this._gameStartedForegroundImage
      .DOFade(0, 1f)
      .OnComplete(() => this._gameStartedForegroundImage.gameObject.SetActive(false));
  }

  private void OnPauseToggled(bool isPaused) {
    this._pauseUI.SetActive(isPaused);
  }

  private void OnGameOver()
  {
    float animDuration = 0.7f;
    Sequence gameOverSequence = DOTween.Sequence();
    gameOverSequence.AppendCallback(() => _postProcessingManager.DeathFX(animDuration));
    gameOverSequence.AppendInterval(animDuration);
    gameOverSequence.AppendCallback(() => {
      this._foregroundImage.color = new Color(0f, 0f, 0f, 0f);
      this._foregroundImage.gameObject.SetActive(true); 
    });
    gameOverSequence.Append(this._foregroundImage.DOFade(1, 0.3f));
    gameOverSequence.AppendCallback(() => {
      if (LeaderboardManager.Instance.CalculatePlacement() > 0) {
        // Show the leaderboard selector menu
        this._leaderboardSetterUI.SetActive(true);
      }
      else {
        ShowGameOver();
      }
    });
    gameOverSequence.AppendInterval(0.5f);
    gameOverSequence.Append(this._foregroundImage.DOFade(0, 1f));
    gameOverSequence.AppendCallback(() => this._foregroundImage.gameObject.SetActive(false));
    gameOverSequence.Play();
  }
}
