using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerSelection
{
    public static T GetObjectReference<T>(float maxDistance, Transform cameraPosition)
    {
        bool raycastHit = Physics.Raycast(cameraPosition.position, cameraPosition.forward, out RaycastHit hit, maxDistance);
        if (raycastHit && hit.collider.TryGetComponent<T>(out T timeBendableObject))
        {
            return timeBendableObject;
        }
        return default;
    }
}
