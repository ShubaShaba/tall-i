using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    
    [SerializeField] private Camera fPcamera;
    [SerializeField] private float maxFocusDistance = 100;
    [SerializeField] private float maxPickingDistance = 2;
    [SerializeField] private float throwForce = 10;

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

    private void ThrowObj(InputAction.CallbackContext context) {
        if (currentPicked != null) {
            currentPicked.Throw(throwForce);
            currentPicked = null;
        }
    }

    private void FocusOnObject(InputAction.CallbackContext context)
    {
        currentFocus?.UnFocus();
        ITimeBody timeBendableObject = GetObjectReference<ITimeBody>(maxFocusDistance);
        timeBendableObject?.Focus();
        currentFocus = timeBendableObject;
    }

    private void RewindAction(InputAction.CallbackContext context)
    {
        currentFocus.StartRewinding();
    }

    private void CancelRewindAction(InputAction.CallbackContext context)
    {
        currentFocus.StopRewiding();
    }

    private void StopTimeAction(InputAction.CallbackContext context)
    {
        currentFocus.StartFreezing();
    }

    private void ResumeTimeAction(InputAction.CallbackContext context)
    {
        currentFocus.StopFreezing();
    }

    private void Start () {
        controlManager.AddPlayersAction(PlayersActionType.Focus, FocusOnObject);
        controlManager.AddPlayersAction(PlayersActionType.Pickup, PickupObj);
        controlManager.AddPlayersAction(PlayersActionType.Throw, ThrowObj);
        controlManager.AddPlayersAction(PlayersActionType.Rewind, RewindAction);
        controlManager.AddPlayersAction(PlayersActionType.CancelRewind, CancelRewindAction);
        controlManager.AddPlayersAction(PlayersActionType.StopTime, StopTimeAction);
        controlManager.AddPlayersAction(PlayersActionType.ResumeTime, ResumeTimeAction);
    }

    private void Update () {
        if (!controlManager.PlayerIsHoldingLeftMouse()) {
            currentPicked?.Throw(0);
            currentPicked = null; 
        }
    }
}
