using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
  [SerializeField] private Image _foregroundCover;
  [SerializeField] private GameObject _scoreboardUI;

  public void LoadGameScene() {
    this._foregroundCover.gameObject.SetActive(true);
    this._foregroundCover
      .DOFade(1, 0.5f)
      .OnComplete(SceneNavigationManager.Instance.LoadGameScene);
  }
  
  public void ShowLeaderboard() {
    this._scoreboardUI.SetActive(true);
  }
  
  public void HideLeaderboard() {
    this._scoreboardUI.SetActive(false);
  }
}
