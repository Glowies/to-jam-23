using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSight : MonoBehaviour
{
    public EyeTarget EyeTarget;
    public int RaycastIgnoreLayer = 6;

    private void FixedUpdate()
    {
        if (IsTargetInSight())
        {
            EyeTarget.ReceiveDamage();
        }
    }

    private bool IsTargetInSight()
    {
        var origin = transform.position;
        var targetPosition = EyeTarget.GetTargetPosition();
        var direction = targetPosition - origin;
        var layerMask = ~(1 << RaycastIgnoreLayer);
        
        // Draw debug ray
        Debug.DrawLine(origin, targetPosition, Color.magenta);
        
        // Cast ray
        var hit = Physics.Raycast(origin, direction, out RaycastHit hitInfo, 1000, layerMask);
            
        // Return true IFF hitting the target collider
        return hit && hitInfo.collider == EyeTarget.GetTargetCollider();
    }
}
