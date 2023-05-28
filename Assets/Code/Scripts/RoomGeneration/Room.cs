using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour {
  [SerializeField] private float _width;
  [SerializeField] private GameObject _frontWall;
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
    foreach (Material frontWallCoverMaterial in this._frontWallCoverMaterials) {
      frontWallCoverMaterial.DOFade(transparency, 0.3f);
    }
  }

  public void Hide() {
    foreach (Material frontWallCoverMaterial in this._frontWallCoverMaterials) {
      frontWallCoverMaterial.DOFade(1f, 2f);
    }
  }

  public GameObject DetachFrontWall() {
    this._frontWall.transform.SetParent(this.transform.parent);
    return this._frontWall.gameObject;
  }

  private void Awake() {
    this.Windows = this._windowTransforms.Select(winTransform => winTransform.position).ToList();

    List<MeshRenderer> frontWallCoverMeshRenderers = this._frontWall.GetComponentsInChildren<MeshRenderer>().ToList();
    print($"Renderers: {frontWallCoverMeshRenderers.Count}");
    this._frontWallCoverMaterials = new List<Material>();
    foreach (MeshRenderer meshRenderer in frontWallCoverMeshRenderers) {
      this._frontWallCoverMaterials.AddRange(meshRenderer.materials);
    }
    print($"Materials: {this._frontWallCoverMaterials.Count}");
  }

  private void Start() {
    this._checkpointTrigger.OnTriggered.AddListener(this.TriggerOnRoomEntered);
  }

  private void TriggerOnRoomEntered() {
    this.OnRoomEntered?.Invoke();
  }
}
