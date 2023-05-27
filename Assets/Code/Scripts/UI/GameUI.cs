using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
  [SerializeField] private Image _foregroundImage;

  public void Initialize(GameManager gameManager) {
    gameManager.OnSetupComplete.AddListener(this.OnGameSetupComplete);
  }

  private void Awake() {
    this._foregroundImage.gameObject.SetActive(true);
  }

  private void OnGameSetupComplete() {
    this._foregroundImage
      .DOFade(0, 0.5f)
      .OnComplete(() => this._foregroundImage.gameObject.SetActive(false));
  }
}
