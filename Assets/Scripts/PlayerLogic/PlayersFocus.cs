using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFocus : MonoBehaviour
{
    [SerializeField] private Camera fPcamera;
    [SerializeField] private float maxFocusDistance = 100;
    [SerializeField] private ControlManager controlManager;
    private CarriableTimeObj currentFocus;

    private CarriableTimeObj getTheObject()
    {
        int layerMask = ~(1 << 6); // Ignore layer 6 (camera itself)

        bool raycastHit = Physics.Raycast(fPcamera.transform.position, fPcamera.transform.forward, out RaycastHit hit, maxFocusDistance, layerMask);
        if (raycastHit && hit.collider.TryGetComponent<CarriableTimeObj>(out CarriableTimeObj timeBendableObject))
        {
            Debug.Log("Focused object: " + timeBendableObject);
            return timeBendableObject;
        }
        return null;
    }

    private void FocusOnObject(InputAction.CallbackContext context)
    {
        currentFocus?.UnFocus();

        CarriableTimeObj timeBendableObject = getTheObject();
        timeBendableObject?.Focus();
        currentFocus = timeBendableObject;
    }

    private void Start () {
        controlManager.AddPlayersAction(PlayersActionType.Focus, FocusOnObject);
    }
}