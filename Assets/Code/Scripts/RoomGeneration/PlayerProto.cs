using UnityEngine;
public class PlayerProto : MonoBehaviour {
  [SerializeField] private float _speed;

  private void Update() {
    float input = Input.GetAxis("Horizontal");
    this.transform.position += Vector3.right * (input * this._speed * Time.deltaTime);
  }
}
