using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalDeathZone : MonoBehaviour
{
    [SerializeField] private float maxTime = 5;
    [SerializeField] private bool isTimed = false;
    [SerializeField] private List<Transform> affectedEntitites = new List<Transform>();
    [SerializeField] private bool self = false;
    private Dictionary<Transform, float> affectedEntititesTimes = new Dictionary<Transform, float>();
    public void Start()
    {
        if (isTimed)
        {
            foreach (Transform entity in affectedEntitites)
                affectedEntititesTimes[entity] = 0;
            return;
        }
        
        if (self) return;
        transform.position = new Vector3(0, -150, 0);
        BoxCollider myCollider = GetComponent<BoxCollider>();
        myCollider.size = new Vector3(1000, 100, 1000);
    }

    private void RespawnObject(Transform objectToRespawn)
    {
        if (objectToRespawn.TryGetComponent<IRespawnable>(out IRespawnable respawnable))
            respawnable.Respawn();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!isTimed)
        {
            RespawnObject(collider.transform);
            return;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (!isTimed) return;

        Transform entity = collider.transform;
        if (affectedEntitites.Contains(entity))
        {
            float timeSpent = affectedEntititesTimes[entity];
            if (timeSpent >= maxTime)
            {
                RespawnObject(entity);
                affectedEntititesTimes[entity] = 0;
            }
            affectedEntititesTimes[entity] += Time.fixedDeltaTime;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (!isTimed) return;
        if (affectedEntitites.Contains(collider.transform))
            affectedEntititesTimes[collider.transform] = 0;
    }
}
