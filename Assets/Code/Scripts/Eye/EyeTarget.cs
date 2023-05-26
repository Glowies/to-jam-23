using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EyeTarget : MonoBehaviour
{
    private Collider _collider;

    void Awake()
    {
        TryGetComponent(out _collider);
    }

    // TODO: Modify what happens when damage is taken every frame
    public void ReceiveDamage() {
        // Debug.Log("Ouch!");
    }

    public Vector3 GetTargetPosition() => transform.position;

    public Collider GetTargetCollider() => _collider;
}
