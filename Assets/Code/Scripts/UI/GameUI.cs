using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
  [SerializeField] private Image _foregroundImage;
  [SerializeField] private GameObject _pauseUI;

  public void Initialize(GameManager gameManager) {
    gameManager.OnSetupComplete.AddListener(this.OnGameSetupComplete);
  }

  private void Awake() {
    this._foregroundImage.gameObject.SetActive(true);
  }

  private void Start() {
    PauseManager.Instance.OnPauseToggled.AddListener(this.OnPauseToggled);
  }

  private void OnGameSetupComplete() {
    this._foregroundImage
      .DOFade(0, 0.5f)
      .OnComplete(() => this._foregroundImage.gameObject.SetActive(false));
  }

  private void OnPauseToggled(bool isPaused) {
    this._pauseUI.SetActive(isPaused);
  }
}
