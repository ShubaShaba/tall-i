using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeathZone
{
    public static Collider[] GetIntesectingColliders(Vector3 position, Quaternion orientation, Collider deathZone, float sensitivity = 0.5f)
    {
        switch (deathZone)
        {
            case BoxCollider dz:
                return BoxZone(position, orientation, dz, sensitivity);
            case SphereCollider dz:
                return SphereZone(position, orientation, dz, sensitivity);
            default:
                throw new ArgumentOutOfRangeException("No implementation of the DEATH_ZONE for this type of collider: " + deathZone.name);
        }
    }

    private static Collider[] BoxZone(Vector3 position, Quaternion orientation, BoxCollider collider, float sensitivity)
    {
        return Physics.OverlapBox(position, collider.size / 2 * sensitivity, orientation);
    }

    private static Collider[] SphereZone(Vector3 position, Quaternion orientation, SphereCollider collider, float sensitivity)
    {
        return Physics.OverlapSphere(position, collider.radius * sensitivity);
    }
}
