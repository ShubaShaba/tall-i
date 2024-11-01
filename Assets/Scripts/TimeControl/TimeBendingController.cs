using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBendingController
{
    private TimeBodyStates currentState;
    private TimeBodyStates previousState;
    private Dictionary<TimeBodyStates, Action> onEnterActions;
    private Dictionary<TimeBodyStates, Action> onExitActions;
    private List<StateInTime> statesInTime;
    private Transform transformRef;
    private Rigidbody rigidbodyRef;
    private float rememberTime;

    public TimeBendingController(float givenRememberTime, Transform givenTransformRef, Rigidbody givenRigidbodyRef)
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

    private void Record()
    {
        if (statesInTime.Count > Mathf.Round(rememberTime / Time.fixedDeltaTime))
        {
            statesInTime.RemoveAt(statesInTime.Count - 1);
        }
        statesInTime.Insert(0, new StateInTime(transformRef.position, transformRef.rotation));
    }

    private void Rewind()
    {
        if (statesInTime.Count > 0)
        {
            StateInTime stateInTime = statesInTime[0];
            statesInTime.RemoveAt(0);
            transformRef.position = stateInTime.position;
            transformRef.rotation = stateInTime.rotation;
        }
        else
        {
            SetState(TimeBodyStates.Natural);
        }
    }
}