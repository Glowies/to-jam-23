using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour {
  [SerializeField] private float _width;
  [SerializeField] private GameObject _frontWall;
  [SerializeField] private MeshRenderer _frontWallCoverMeshRenderer;
  [SerializeField] private CheckpointTrigger _checkpointTrigger;

  public float Width => this._width;
  public UnityEvent OnRoomEntered;

  private Material _frontWallCoverMaterial;

  public Vector3 RightEdge {
    get {
      Vector3 rightEdge = this.transform.position;
      rightEdge.x += this._width / 2f;
      return rightEdge;
    }
  }

  public void Show(float transparency = 0f) {
    this._frontWallCoverMaterial.DOFade(transparency, 0.3f);
  }

  public void Hide() {
    this._frontWallCoverMaterial.DOFade(1f, 0.3f);
  }

  public GameObject DetachFrontWall() {
    this._frontWall.transform.SetParent(this.transform.parent);
    return this._frontWall.gameObject;
  }

  private void Awake() {
    this._frontWallCoverMaterial = this._frontWallCoverMeshRenderer.material;
  }

  private void Start() {
    this._checkpointTrigger.OnTriggered.AddListener(this.TriggerOnRoomEntered);
  }

  private void TriggerOnRoomEntered() {
    this.OnRoomEntered?.Invoke();
  }
}
