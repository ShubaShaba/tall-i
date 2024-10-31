using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarriableTimeObj : CarriableBase, ITimeBody
{
    [SerializeField] private ControlManager controlManager;
    private TimeBodyStates currentState;
    private TimeBodyStates previousState;
    private List<PointInTime> pointsInTime;

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

    public void StopRewiding()
    {
        SetState(TimeBodyStates.Natural);
        rb.isKinematic = false;
    }

    public void StartFreezing()
    {
        SetState(TimeBodyStates.Stoped);
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezePosition;
    }

    public void StopFreezing()
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

    public override bool CanPick() {
        return currentState == TimeBodyStates.Natural;
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

    private void Start()
    {
        currentState = TimeBodyStates.Natural;
        pointsInTime = new List<PointInTime>();
    }

    void Update()
    {
        HandleTime();
    }
}
