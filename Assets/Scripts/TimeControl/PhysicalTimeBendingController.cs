using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalTimeBendingController
{
    private TimeBodyStates currentState;
    private TimeBodyStates previousState;
    private Dictionary<TimeBodyStates, Action> onEnterActions;
    private Dictionary<TimeBodyStates, Action> onExitActions;
    private List<StateInTime> statesInTime;
    private Transform transformRef;
    private Rigidbody rigidbodyRef;
    private float rememberTime;

    public PhysicalTimeBendingController(float givenRememberTime, Transform givenTransformRef, Rigidbody givenRigidbodyRef)
    {
        statesInTime = new List<StateInTime>();
        onExitActions = new Dictionary<TimeBodyStates, Action>();
        onEnterActions = new Dictionary<TimeBodyStates, Action>();
        rememberTime = givenRememberTime;
        transformRef = givenTransformRef;
        rigidbodyRef = givenRigidbodyRef;
    }

    public void AddOnExitAction(TimeBodyStates state, Action action)
    {
        onExitActions.Add(state, action);
    }

    public void AddOnEnterAction(TimeBodyStates state, Action action)
    {
        onEnterActions.Add(state, action);
    }

    public void SetState(TimeBodyStates state)
    {
        if (state == currentState)
            return;

        if (onExitActions.ContainsKey(currentState))
        {
            onExitActions[currentState]();
        }

        previousState = currentState;
        currentState = state;

        if (onEnterActions.ContainsKey(currentState))
        {
            onEnterActions[currentState]();
        }
    }

    public TimeBodyStates GetCurrentState()
    {
        return currentState;
    }

    public TimeBodyStates GetPreviousState()
    {
        return previousState;
    }

    public void HandleTime()
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

    public void setPreviousPhysicalState()
    {
        if (statesInTime.Count > 0)
        {
            rigidbodyRef.velocity = statesInTime[0].linearVelocity;
            rigidbodyRef.angularVelocity = statesInTime[0].angularVelocity;
        }
    }

    private void Record()
    {
        if (statesInTime.Count > Mathf.Round(rememberTime / Time.fixedDeltaTime))
        {
            statesInTime.RemoveAt(statesInTime.Count - 1);
        }
        statesInTime.Insert(0, new StateInTime(
            transformRef.position, transformRef.rotation,
            rigidbodyRef.velocity, rigidbodyRef.angularVelocity
        ));
    }

    private void Rewind()
    {
        if (statesInTime.Count > 0)
        {
            StateInTime stateInTime = statesInTime[0];
            statesInTime.RemoveAt(0);

            rigidbodyRef.MovePosition(stateInTime.position + (-stateInTime.linearVelocity) * Time.fixedDeltaTime);
            Quaternion deltaRotation = Quaternion.Euler((-stateInTime.angularVelocity) * Time.fixedDeltaTime);
            rigidbodyRef.MoveRotation(stateInTime.rotation * deltaRotation);
        }
        else
        {
            SetState(TimeBodyStates.Natural);
        }
    }
}