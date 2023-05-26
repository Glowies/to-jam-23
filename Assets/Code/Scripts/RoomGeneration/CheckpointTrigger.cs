using UnityEngine;
using UnityEngine.Events;

public class CheckpointTrigger : MonoBehaviour {
  [SerializeField] private bool _disableOnceTriggered = true;
  public UnityEvent OnTriggered;

  private void OnTriggerEnter(Collider other) {
    if (!other.CompareTag("Player"))
      return;

    this.OnTriggered?.Invoke();

    if (this._disableOnceTriggered)
      this.gameObject.SetActive(false);
  }
}
