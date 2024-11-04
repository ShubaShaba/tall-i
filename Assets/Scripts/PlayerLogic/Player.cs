using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, ICarrier
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

    private void PickupObj(InputAction.CallbackContext context)
    {
        ICarriable carriableObj = GetObjectReference<ICarriable>(maxPickingDistance);
        if (carriableObj != null)
        {
            currentPicked?.Throw(0);
            carriableObj.Pickup(this);
        }
    }

    private void ThrowObj(InputAction.CallbackContext context)
    {
        currentPicked?.Throw(throwForce);
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
        currentFocus?.ToggleRewind();
    }

    private void StopTimeAction(InputAction.CallbackContext context)
    {
        currentFocus?.ToggleFreeze();
    }

    private void ManualControlAction(InputAction.CallbackContext context)
    {
        currentFocus?.ToggleManualControl();
    }

    private void ManualBackward(InputAction.CallbackContext context)
    {
        currentFocus?.ManualBackward();
    }

    private void ManualForward(InputAction.CallbackContext context)
    {
        currentFocus?.ManualForward();
    }

    private void Start()
    {
        controlManager.AddPlayersAction(PlayersActionType.Focus, FocusOnObject);
        controlManager.AddPlayersAction(PlayersActionType.Pickup, PickupObj);
        controlManager.AddPlayersAction(PlayersActionType.Throw, ThrowObj);
        controlManager.AddPlayersAction(PlayersActionType.Rewind, RewindAction);
        controlManager.AddPlayersAction(PlayersActionType.StopTime, StopTimeAction);
        controlManager.AddPlayersAction(PlayersActionType.ManualRewind, ManualControlAction);
        controlManager.AddPlayersAction(PlayersActionType.StartManualRewind, ManualBackward);
        controlManager.AddPlayersAction(PlayersActionType.StartManualReverseRewind, ManualForward);
    }

    // TODO: fix: control releated stuff is not supposed to be in Player.cs
    private void Update()
    {
        if (!controlManager.PlayerIsHoldingLeftMouse())
        {
            currentPicked?.Throw(0);
        }

        if (currentFocus == null) return;

        if (!controlManager.PlayerIsRewindingBackward() && !controlManager.PlayerIsRewindingForward() && currentFocus.IsInManualMode())
        {
            currentFocus?.ToggleManualControl();
        }
    }

    public void AddCarriable(ICarriable obj)
    {
        currentPicked = obj;
    }

    public void RemoveCarriable(ICarriable obj)
    {
        currentPicked = null;
    }

    public Transform GetMountingPointTransform()
    {
        return mountingPoint;
    }
}
