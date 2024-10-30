using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarriableTimeObj : MonoBehaviour, TimeBody
{
    [SerializeField] private ControlManager controlManager;
    private TimeBodyStates currentState;
    private TimeBodyStates previousState;
    private List<PointInTime> pointsInTime;

    private Rigidbody rb;

    private bool isFocusedOn = false;

    public void Focus()
    {
        isFocusedOn = true;
    }

    public void UnFocus()
    {
        isFocusedOn = false;
        StopFreezing();
        StopRewiding();
    }

    public void StartRewinding()
    {
        SetState(TimeBodyStates.Rewinding);
        rb.isKinematic = true;
    }

    private void SetState(TimeBodyStates state)
    {
        if (state == currentState)
            return;

        previousState = currentState;
        currentState = state;
    }

    private void Record()
    {
        if (pointsInTime.Count > Mathf.Round(5f / Time.deltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    private void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewiding();
        }
    }

    private void StopRewiding()
    {
        SetState(TimeBodyStates.Natural);
        rb.isKinematic = false;
    }

    private void StartFreezing()
    {
        SetState(TimeBodyStates.Stoped);
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezePosition;
    }

    private void StopFreezing()
    {
        rb.constraints = 0;
        if (previousState == TimeBodyStates.Rewinding)
        {
            StartRewinding();
        }
        else
        {
            StopRewiding();
        }
    }

    private void HandleTime()
    {
        switch (currentState)
        {
            case TimeBodyStates.Natural:
                Record();
                break;
            case TimeBodyStates.Rewinding:
                Rewind();
                break;
            default:
                break;
        }
    }

    // CONTROL SCHEMA:
    private void RewindAction(InputAction.CallbackContext context)
    {
        if (!isFocusedOn)
            return;
        StartRewinding();
    }

    private void CancelRewindAction(InputAction.CallbackContext context)
    {
        if (!isFocusedOn)
            return;
        StopRewiding();
    }

    private void StopTimeAction(InputAction.CallbackContext context)
    {
        if (!isFocusedOn)
            return;
        StartFreezing();
    }

    private void ResumeTimeAction(InputAction.CallbackContext context)
    {
        if (!isFocusedOn)
            return;
        StopFreezing();
    }

    // START-UPDATE methods

    private void Start()
    {
        currentState = TimeBodyStates.Natural;
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
        controlManager.AddPlayersAction(PlayersActionType.Rewind, RewindAction);
        controlManager.AddPlayersAction(PlayersActionType.CancelRewind, CancelRewindAction);
        controlManager.AddPlayersAction(PlayersActionType.StopTime, StopTimeAction);
        controlManager.AddPlayersAction(PlayersActionType.ResumeTime, ResumeTimeAction);
    }

    void Update()
    {
        HandleTime();
    }
}
