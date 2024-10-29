using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject target;
    public Camera cam;

    public bool IsVisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        Vector3 point = target.transform.position;

        foreach (Plane plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
                return false;
        }
        return true;
    }
}