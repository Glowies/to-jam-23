using Controls;
using UnityEngine;
using UnityEngine.UI;

public class ControlsUI : MonoBehaviour {
    [SerializeField] private Image _stopOutlineImage;
    [SerializeField] private Image _walkOutlineImage;
    [SerializeField] private Image _runOutlineImage;
    private PlayerController _playerController;

    public void Initialize(PlayerController playerController) {
        this._playerController = playerController;
        this._playerController.OnPlayerStateChanged.AddListener(this.OnPlayerStateChanged);
    }

    private void OnPlayerStateChanged(PlayerState playerState) {
        this._stopOutlineImage.color = this.GetHighlightColor(playerState is HidingState);
        this._walkOutlineImage.color = this.GetHighlightColor(playerState is WalkingState);
        this._runOutlineImage.color = this.GetHighlightColor(playerState is RunningState);
    }

    private Color GetHighlightColor(bool isActive) {
        return isActive ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);
    }
}
