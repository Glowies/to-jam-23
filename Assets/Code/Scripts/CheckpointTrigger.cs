using UnityEngine;
using UnityEngine.Events;

public class CheckpointTrigger : MonoBehaviour {
  public UnityEvent OnTriggered;

  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {
      this.OnTriggered?.Invoke();
    }
  }
}
