using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationalPlatformBase : MovingPlatformBase
{
    [SerializeField] private Transform rotationPoint;
    [SerializeField] private Vector3 rotationDirection;
    private bool isSync = false;
    protected override void MoveInFixedUpdate()
    {
        if (isMoving)
        {
            Quaternion deltaRotation = Quaternion.Euler(rotationDirection.normalized * speed[currentTarget] * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        CarryObjects();
        lastFramePos = rotationPoint.position;
    }

    protected override void CarryObjects()
    {
        bool playerIsPresent = false;
        Collider[] hitColliders = Physics.OverlapBox(rb.position + carryDetectionOffset, carryDetectionSize, Quaternion.identity);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].TryGetComponent<Player>(out Player childNodeTr))
            {
                playerIsPresent = true;
                if (!isSync)
                {
                    rotationPoint.transform.position = childNodeTr.transform.position;
                    isSync = true;
                }
                else
                    childNodeTr.transform.position = childNodeTr.transform.position + (rotationPoint.position - lastFramePos);
            }
        }
        isSync = playerIsPresent;
    }

    protected void ResetPlayer() { isSync = false ;}
}
