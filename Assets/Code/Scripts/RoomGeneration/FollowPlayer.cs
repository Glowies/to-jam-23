using UnityEngine;
public class FollowPlayer : MonoBehaviour {
  [SerializeField] private Transform _playerTransform;
  [SerializeField] private Vector3 _offset;
  private Transform _transform;

  private void Awake() {
    this._transform = this.transform;
  }

  private void Update() {
    this._transform.position = this._playerTransform.position + this._offset;
  }
}
