using Controls;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Doors : MonoBehaviour {
  [SerializeField] private Transform _explosionForceOrigin;
  [SerializeField] private float _walkForce;
  [SerializeField] private float _runForce;
  [SerializeField] private float _explosionRaduis;
  [SerializeField] private List<Rigidbody> _doorRigidbodies;

  private List<float> _doorMinLimits;
  private List<float> _doorMaxLimits;

  private void Awake() {
    this._doorMinLimits = new List<float>();
    this._doorMaxLimits = new List<float>();

    List<HingeJoint> doorHinges = this._doorRigidbodies.Select(door => door.GetComponent<HingeJoint>()).ToList();
    for (int i = 0; i < doorHinges.Count; i++) {
      HingeJoint doorHinge = doorHinges[i];
      float doorInitRotation = this._doorRigidbodies[i].transform.eulerAngles.y;
      this._doorMinLimits.Add((doorHinge.limits.min + doorInitRotation) % 360);
      this._doorMaxLimits.Add((doorHinge.limits.max + doorInitRotation) % 360);
    }
  }

  private void FixedUpdate() {
    for (int i = 0; i < this._doorRigidbodies.Count; i++) {
      Rigidbody doorRigidbody = this._doorRigidbodies[i];
      if (this.DoorApproachedLimit(doorRigidbody, this._doorMinLimits[i], this._doorMaxLimits[i])) {
        doorRigidbody.isKinematic = true;
        doorRigidbody.detectCollisions = false;
      }
    }
  }

  private bool DoorApproachedLimit(Rigidbody door, float min, float max) {
    Vector3 eulerAngles = door.transform.eulerAngles;
    return Mathf.Abs(eulerAngles.y - min) < 2f || Mathf.Abs(eulerAngles.y - max) < 2f;
  }

  private void OnTriggerEnter(Collider other) {
    if (!other.gameObject.CompareTag("Player"))
      return;

    PlayerController playerController = other.GetComponent<PlayerController>();
    if (!playerController)
      return;

    float explosionForce = playerController.GetPlayerState() is RunningState ? this._runForce : this._walkForce;
    foreach (Rigidbody doorRigidbody in this._doorRigidbodies) {
      doorRigidbody.AddExplosionForce(explosionForce, this._explosionForceOrigin.position, this._explosionRaduis);
    }
  }
}
