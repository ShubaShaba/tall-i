using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting;

public class ControlManager : MonoBehaviour
{
    [SerializeField] GameObject playerReference;
    private PlayersControl inputActions;
    private PlayersControl.PlayerActions playerActions;
   

    private void Awake()
    {
        inputActions = new PlayersControl();
        playerActions = inputActions.Player;
        playerActions.Enable();
    }

    public Vector2 GetInputDirectionNormalized()
    {
        return playerActions.Movement.ReadValue<Vector2>();
    }

    // Does not work as good (TODO: fix later)
    // public Vector2 GetMouseMovement() {
    //     return new Vector2(playerActions.CameraX.ReadValue<float>(), playerActions.CameraY.ReadValue<float>()).normalized;
    // }

    public bool PlayerIsHoldingLeftMouse()
    {
        return playerActions.Interact.ReadValue<float>() >= 1.0;
    }

    public bool PlayerIsRunning()
    {
        return playerActions.Sprint.ReadValue<float>() >= 1.0;
    }

    public bool PlayerIsRewindingBackward()
    {
        return playerActions.ManualBackward.ReadValue<float>() >= 1.0;
    }

    public bool PlayerIsRewindingForward()
    {
        return playerActions.ManualForward.ReadValue<float>() >= 1.0;
    }

    public void AddPlayersAction(PlayersActionType type, Action<InputAction.CallbackContext> action)
    {
        switch (type)
        {
            case PlayersActionType.Jump:
                playerActions.Jump.performed += action;
                break;
            case PlayersActionType.Focus:
                playerActions.Focus.performed += action;
                break;
            case PlayersActionType.Rewind:
                playerActions.RewindTime.performed += action;
                break;
            case PlayersActionType.StopTime:
                playerActions.StopTime.performed += action;
                break;
            case PlayersActionType.Interact:
                playerActions.Interact.performed += action;
                break;
            case PlayersActionType.Throw:
                playerActions.Throw.performed += action;
                break;
            case PlayersActionType.ManualRewind:
                playerActions.ManualRewind.performed += action;
                break;
            case PlayersActionType.StartManualRewind:
                playerActions.ManualBackward.performed += action;
                break;
            case PlayersActionType.StartManualReverseRewind:
                playerActions.ManualForward.performed += action;
                break;
        }
    }

    private void OnDestroy()
    {
        inputActions.Dispose();
    }
}
