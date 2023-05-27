using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSight : MonoBehaviour
{
    public EyeTarget EyeTarget;
    public EyeBT eyeBT;
    public int RaycastIgnoreLayer = 6;

    [SerializeField] private float windowWidth = 1f;

    private float test = 0;
    private bool testBool = false;

    private void FixedUpdate()
    {
        if (IsTargetInSight())
        {
            EyeTarget.ReceiveDamage();
        }
    }

    private void OnDrawGizmos()
    {
        object currWindowPos = eyeBT.getFocusedWindow();
        if (currWindowPos != null)
        {
            Gizmos.color = Color.red;
            Vector3 relativePos = (Vector3)currWindowPos + new Vector3(test, 0, 0);
            Vector3 delta = new Vector3(EyeTarget.targetWidth * 0.5f, 0, 0);
            Gizmos.DrawSphere(relativePos, 0.1f);
            Gizmos.DrawLine(relativePos - delta, relativePos + delta);

            Gizmos.color = (testBool) ? Color.red : Color.grey;
            delta = new Vector3(windowWidth * 0.5f, 0, 0);
            Gizmos.DrawLine((Vector3)currWindowPos - delta, (Vector3)currWindowPos + delta);
        }
        

    }

    public bool IsTargetInSight()
    {
        var origin = transform.position;
        var targetPosition = EyeTarget.GetTargetPosition();
        var direction = targetPosition - origin;
        var layerMask = ~(1 << RaycastIgnoreLayer);


        // make sure player is in front of focused window

        
        if (eyeBT.getFocusedWindow() == null) return false;
        Vector3 windowPos = (Vector3)eyeBT.getFocusedWindow(); // get window eye is in front of

        // basic geom to get projected distance
        float zDistOfEyeToWindow = transform.position.z - windowPos.z;
        float xDistOfPlayerToWindow = windowPos.x - EyeTarget.GetTargetPosition().x;
        float zDistOfPlayerToEye = transform.position.z - EyeTarget.GetTargetPosition().z;

        float playerXPosProjectedToWindowZ = -zDistOfEyeToWindow * xDistOfPlayerToWindow / zDistOfPlayerToEye;
        test = playerXPosProjectedToWindowZ;

        float targetWidth = EyeTarget.targetWidth;
        float start = Mathf.Max(playerXPosProjectedToWindowZ - targetWidth * 0.5f, -windowWidth * 0.5f);
        float end = Mathf.Min(playerXPosProjectedToWindowZ + targetWidth * 0.5f, windowWidth * 0.5f);
        //bool playerInCurrentWindowView = playerXPosProjectedToWindowZ < windowWidth * 0.5f;

        bool playerInCurrentWindowView = start <= end;
        testBool = playerInCurrentWindowView;
        if (!playerInCurrentWindowView) return false;



        
        
        // Cast ray
        var hit = Physics.Raycast(origin, direction, out RaycastHit hitInfo, 1000, layerMask);

        // Draw debug ray
        Debug.DrawLine(origin, targetPosition, (hit && hitInfo.collider == EyeTarget.GetTargetCollider()) ? Color.magenta : Color.grey);
        //Debug.Log(hitInfo.collider.gameObject.name + ", " + (hit && hitInfo.collider == EyeTarget.GetTargetCollider()) + ", " + hit);

        // Return true IFF hitting the target collider
        return hit && hitInfo.collider == EyeTarget.GetTargetCollider();
    }
}
