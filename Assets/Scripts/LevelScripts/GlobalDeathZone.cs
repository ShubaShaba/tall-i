using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalDeathZone : MonoBehaviour
{

    public void Start()
    {
        transform.position = new Vector3(0, -150, 0);
        BoxCollider myCollider = GetComponent<BoxCollider>();
        myCollider.size = new Vector3 (1000, 100, 1000); 
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<IRespawnable>(out IRespawnable objectToRespawn))
            objectToRespawn.Respawn();
    }

    // void OnTriggerStay(Collider collider)
    // {
    //     OnTriggerEnter(collider);
    // }
}
