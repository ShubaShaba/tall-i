using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    
    [SerializeField] private Camera fPcamera;
    [SerializeField] private float maxFocusDistance = 100;
    [SerializeField] private float maxPickingDistance = 2;

    [SerializeField] private ControlManager controlManager;
    [SerializeField] private Transform mountingPoint;
    private ITimeBody currentFocus;
    private ICarriable currentPicked;

    private T GetObjectReference<T>(float maxDistance)
    {
        bool raycastHit = Physics.Raycast(fPcamera.transform.position, fPcamera.transform.forward, out RaycastHit hit, maxDistance);
        if (raycastHit && hit.collider.TryGetComponent<T>(out T timeBendableObject))
        {
            return timeBendableObject;
        }
        return default;
    }

    private void PickupObj(InputAction.CallbackContext context) {
        ICarriable carriableObj = GetObjectReference<ICarriable>(maxPickingDistance);
        if (carriableObj != null) {
            currentPicked?.Throw(0);
            currentPicked = carriableObj;
            currentPicked.Pickup(mountingPoint);
        }
    }

    private void FocusOnObject(InputAction.CallbackContext context)
    {
        currentFocus?.UnFocus();
        ITimeBody timeBendableObject = GetObjectReference<ITimeBody>(maxFocusDistance);
        timeBendableObject?.Focus();
        currentFocus = timeBendableObject;
    }

    private void Start () {
        controlManager.AddPlayersAction(PlayersActionType.Focus, FocusOnObject);
        controlManager.AddPlayersAction(PlayersActionType.Pickup, PickupObj);
    }

    private void Update () {
        if (!controlManager.PlayerIsHoldingLeftMouse()) {
            currentPicked?.Throw(0);
            currentPicked = null; 
        }
    }
}
