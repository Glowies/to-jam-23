using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour {
  [SerializeField] private float _width;
  [SerializeField] private GameObject _frontWall;
  // [SerializeField] private List<MeshRenderer> _frontWallCoverMeshRenderers;
  [SerializeField] private CheckpointTrigger _checkpointTrigger;
  [SerializeField] private List<Transform> _windowTransforms;

  public float Width => this._width;
  public UnityEvent OnRoomEntered;
  public List<Vector3> Windows;

  private List<Material> _frontWallCoverMaterials;

  public Vector3 RightEdge {
    get {
      Vector3 rightEdge = this.transform.position;
      rightEdge.x += this._width / 2f;
      return rightEdge;
    }
  }

  public void Show(float transparency = 0f) {
    // this._frontWallCoverMaterials.DOFade(transparency, 0.3f);
    this._frontWall.SetActive(false);
  }

  public void Hide() {
    // this._frontWallCoverMaterials.DOFade(1f, 0.3f);
    this._frontWall.SetActive(true);
  }

  public GameObject DetachFrontWall() {
    this._frontWall.transform.SetParent(this.transform.parent);
    return this._frontWall.gameObject;
  }

  private void Awake() {
    // this._frontWallCoverMaterials = this._frontWallCoverMeshRenderers.material;
    this.Windows = this._windowTransforms.Select(winTransform => winTransform.position).ToList();
  }

  private void Start() {
    this._checkpointTrigger.OnTriggered.AddListener(this.TriggerOnRoomEntered);
  }

  private void TriggerOnRoomEntered() {
    this.OnRoomEntered?.Invoke();
  }
}
