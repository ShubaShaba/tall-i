using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFocus : MonoBehaviour
{
    [SerializeField] private Camera fPcamera;
    [SerializeField] private float maxFocusDistance = 100;
    [SerializeField] private TimeBody currentFocus;

    public TimeBody getTheObject() {
        int layerMask = ~(1 << 6); // Ignore layer 6 (camera itself)

        bool raycastHit = Physics.Raycast(fPcamera.transform.position, fPcamera.transform.forward, out RaycastHit hit, maxFocusDistance, layerMask);
        if (raycastHit && hit.collider.TryGetComponent<TimeBody>(out TimeBody timeBendableObject)) {
            Debug.Log("Focused object: " + timeBendableObject);
            return timeBendableObject;
        }
        return null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            currentFocus?.UnFocus();
            
            TimeBody timeBendableObject = getTheObject(); 
            timeBendableObject?.Focus();
            currentFocus = timeBendableObject;
        }
    }
}