using UnityEngine;

public class FollowTarget : MonoBehaviour {
  [SerializeField] private Transform _transformTransform;
  [SerializeField] private Vector3 _offset;
  private Transform _transform;

  private void Awake() {
    this._transform = this.transform;
  }

  private void Update() {
    if (!this._transformTransform) return;

    this._transform.position = this._transformTransform.position + this._offset;
  }
}
