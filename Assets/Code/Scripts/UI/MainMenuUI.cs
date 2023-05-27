using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
  [SerializeField] private Image _foregroundCover;

  public void LoadGameScene() {
    this._foregroundCover.gameObject.SetActive(true);
    this._foregroundCover
      .DOFade(1, 0.5f)
      .OnComplete(SceneNavigationManager.Instance.LoadGameScene);
  }
}
