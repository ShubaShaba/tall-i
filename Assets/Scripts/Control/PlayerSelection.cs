using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerSelection
{
    public static T GetObjectReference<T>(float maxDistance, Transform cameraPosition)
    {
        bool raycastHit = Physics.Raycast(cameraPosition.position, cameraPosition.forward, out RaycastHit hit, maxDistance);
        if (raycastHit && hit.collider.TryGetComponent<T>(out T timeBendableObject))
            return timeBendableObject;

        return default;
    }

    public static T GetObjectReferenceImproved<T>(float maxDistance, Transform cameraPosition, bool isProximity = false)
    {
        RaycastHit[] raycastHit;

        if (isProximity)
            raycastHit = Physics.SphereCastAll(cameraPosition.position, maxDistance, cameraPosition.forward, 0);
        else
            raycastHit = Physics.SphereCastAll(cameraPosition.position + cameraPosition.forward * maxDistance, maxDistance / 6, cameraPosition.forward, 0);

        for (int i = 0; i < raycastHit.Length; i++)
        {
            if (raycastHit[i].collider.TryGetComponent<T>(out T timeBendableObject))
                return timeBendableObject;
        }
        return default;
    }

    // TEMP:

    public static Transform GetObjectReferenceImprovedTransform<T>(float maxDistance, Transform cameraPosition, bool isProximity = false)
    {
        RaycastHit[] raycastHit;

        if (isProximity)
            raycastHit = Physics.SphereCastAll(cameraPosition.position, maxDistance, cameraPosition.forward, 0);
        else
            raycastHit = Physics.SphereCastAll(cameraPosition.position + cameraPosition.forward * maxDistance, maxDistance / 6, cameraPosition.forward, 0);

        for (int i = 0; i < raycastHit.Length; i++)
        {
            if (raycastHit[i].collider.TryGetComponent<T>(out T timeBendableObject))
                return raycastHit[i].transform;
        }
        return default;
    }
}
