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

    public static T GetObjectReferenceImproved<T>(float maxDistance, Transform cameraPosition)
    {
        RaycastHit[] raycastHit = Physics.SphereCastAll(cameraPosition.position + cameraPosition.forward * maxDistance, maxDistance / 6, cameraPosition.forward, 0);
        for (int i = 0; i < raycastHit.Length; i++)
        {
            // Debug.Log(raycastHit[i].collider.name);
            if (raycastHit[i].collider.TryGetComponent<T>(out T timeBendableObject))
                return timeBendableObject;
        }
        return default;
    }
}
