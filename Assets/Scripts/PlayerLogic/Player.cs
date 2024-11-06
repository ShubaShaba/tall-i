using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, ICarrier, IPlayerUI
{

    [SerializeField] private Transform cameraPosition;
    [SerializeField] private float maxFocusDistance = 100;
    [SerializeField] private float maxInteractionDistance = 2;
    [SerializeField] private float throwForce = 10;

    [SerializeField] private ControlManager controlManager;
    [SerializeField] private Transform mountingPoint;
    private ITimeBody currentFocus;
    private ICarriable currentPicked;

    private void PickupObj(InputAction.CallbackContext context)
    {
        if (currentPicked != null) return;

        ICarriable carriableObj = PlayerSelection.GetObjectReference<ICarriable>(maxInteractionDistance, cameraPosition);
        if (carriableObj == null)
            carriableObj = PlayerSelection.GetObjectReferenceImproved<ICarriable>(maxInteractionDistance, cameraPosition);

        if (carriableObj != null)
            carriableObj.Pickup(this);
    }

    private void ThrowObj(InputAction.CallbackContext context)
    {
        currentPicked?.Throw(cameraPosition.forward * throwForce, false);
    }

    private void InteractWithObj(InputAction.CallbackContext context)
    {
        IInteractable interactableObj = PlayerSelection.GetObjectReference<IInteractable>(maxInteractionDistance, cameraPosition);
        interactableObj?.Interact();
    }

    private void EjectCarriableFromObject(InputAction.CallbackContext context)
    {
        ICarrier anotherCarrier = PlayerSelection.GetObjectReference<ICarrier>(maxInteractionDistance, cameraPosition);
        anotherCarrier?.Eject();
    }

    private void FocusOnObject(InputAction.CallbackContext context)
    {
        currentFocus?.UnFocus();
        ITimeBody timeBendableObject = PlayerSelection.GetObjectReference<ITimeBody>(maxFocusDistance, cameraPosition);
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
        controlManager.AddPlayersAction(PlayersActionType.Interact, PickupObj);
        controlManager.AddPlayersAction(PlayersActionType.Interact, InteractWithObj);
        controlManager.AddPlayersAction(PlayersActionType.Throw, ThrowObj);
        controlManager.AddPlayersAction(PlayersActionType.Throw, EjectCarriableFromObject);
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
            currentPicked?.Throw(Vector3.zero, false);
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

    public void Eject()
    {
        currentPicked?.Throw(Vector3.zero, true);
    }

    public ICarriable GetCarriable()
    {
        return currentPicked;
    }

    public bool isCarryingSomething()
    {
        return currentPicked != null;
    }

    public bool isFocusedOnSomethingType1()
    {
        return currentFocus != null;
    }

    public bool isFocusedOnSomethingType2()
    {
        return currentFocus != null;
    }

}
