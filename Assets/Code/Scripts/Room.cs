using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour {
  [SerializeField] private float _width;
  [SerializeField] private GameObject _frontWall;
  [SerializeField] private CheckpointTrigger _checkpointTrigger;

  public float Width => this._width;
  public UnityEvent OnRoomEntered;

  public Vector3 RightEdge {
    get {
      Vector3 rightEdge = this.transform.position;
      rightEdge.x += this._width / 2f;
      return rightEdge;
    }
  }

  public void Show() {
    this._frontWall.SetActive(false);
  }

  public void Hide() {
    this._frontWall.SetActive(true);
  }

  private void Start() {
    this._checkpointTrigger.OnTriggered.AddListener(this.TriggerOnRoomEntered);
  }

  private void TriggerOnRoomEntered() {
    this.OnRoomEntered?.Invoke();
  }
}
