using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFocus : MonoBehaviour
{
    [SerializeField] private Camera fPcamera;
    [SerializeField] private float maxFocusDistance = 100;
    [SerializeField] private ControlManager controlManager;
    private TimeBody currentFocus;

    private TimeBody getTheObject()
    {
        int layerMask = ~(1 << 6); // Ignore layer 6 (camera itself)

        bool raycastHit = Physics.Raycast(fPcamera.transform.position, fPcamera.transform.forward, out RaycastHit hit, maxFocusDistance, layerMask);
        if (raycastHit && hit.collider.TryGetComponent<TimeBody>(out TimeBody timeBendableObject))
        {
            Debug.Log("Focused object: " + timeBendableObject);
            return timeBendableObject;
        }
        return null;
    }

    private void FocusOnObject(InputAction.CallbackContext context)
    {
        currentFocus?.UnFocus();

        TimeBody timeBendableObject = getTheObject();
        timeBendableObject?.Focus();
        currentFocus = timeBendableObject;
    }

    private void Start () {
        controlManager.AddPlayersAction(PlayersActionType.Focus, FocusOnObject);
    }
}