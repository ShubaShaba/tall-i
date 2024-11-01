using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalTimeBendingController
{
    private TimeBodyStates currentState;
    private TimeBodyStates previousState;
    private Dictionary<TimeBodyStates, List<Action>> onEnterActions;
    private Dictionary<TimeBodyStates, List<Action>> onExitActions;
    private List<StateInTime> statesInTime;
    private Transform transformRef;
    private Rigidbody rigidbodyRef;
    private float rememberTime;

    private int rewindIndex = 0;

    public PhysicalTimeBendingController(float givenRememberTime, Transform givenTransformRef, Rigidbody givenRigidbodyRef)
    {
        statesInTime = new List<StateInTime>();
        onExitActions = new Dictionary<TimeBodyStates, List<Action>>();
        onEnterActions = new Dictionary<TimeBodyStates, List<Action>>();
        rememberTime = givenRememberTime;
        transformRef = givenTransformRef;
        rigidbodyRef = givenRigidbodyRef;

        AddOnEnterAction(TimeBodyStates.Rewinding, onEnterRewind);
        AddOnEnterAction(TimeBodyStates.ReverseRewinding, onEnterRewind);
        AddOnEnterAction(TimeBodyStates.Stoped, onEnterFreeze);
        AddOnExitAction(TimeBodyStates.Rewinding, OnExitRewind);
        AddOnExitAction(TimeBodyStates.ReverseRewinding, OnExitRewind);
        AddOnExitAction(TimeBodyStates.Stoped, OnExitFreeze);
    }

    public void AddOnExitAction(TimeBodyStates state, Action action)
    {
        if (onExitActions.ContainsKey(state))
        {
            onExitActions[state].Add(action);
        }
        else
        {
            onExitActions.Add(state, new List<Action>() { action });
        }
    }

    public void AddOnEnterAction(TimeBodyStates state, Action action)
    {
        if (onEnterActions.ContainsKey(state))
        {
            onEnterActions[state].Add(action);
        }
        else
        {
            onEnterActions.Add(state, new List<Action>() { action });
        }
    }

    public void SetState(TimeBodyStates state)
    {
        if (state == currentState)
            return;

        if (onExitActions.ContainsKey(currentState))
        {
            foreach (Action action in onExitActions[currentState])
                action();
        }

        previousState = currentState;
        currentState = state;

        if (onEnterActions.ContainsKey(currentState))
        {
            foreach (Action action in onEnterActions[currentState])
                action();
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
                if (rewindIndex > 0)
                {
                    SetState(TimeBodyStates.ReverseRewinding);
                }
                else
                {
                    Record();
                }
                break;
            case TimeBodyStates.Rewinding:
                Rewind();
                break;
            case TimeBodyStates.ReverseRewinding:
                ReverseRewind();
                break;
            default:
                break;
        }
    }

    private void SetPreviousPhysicalState()
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
        if (rewindIndex < statesInTime.Count)
        {
            StateInTime stateInTime = statesInTime[rewindIndex];

            rigidbodyRef.MovePosition(stateInTime.position + (-stateInTime.linearVelocity) * Time.fixedDeltaTime);
            Quaternion deltaRotation = Quaternion.Euler((-stateInTime.angularVelocity) * Time.fixedDeltaTime);
            rigidbodyRef.MoveRotation(stateInTime.rotation * deltaRotation);

            rewindIndex++;
        }
        else
        {
            statesInTime.Clear();
            rewindIndex = 0;
            SetState(TimeBodyStates.Natural);
        }
    }

    private void ReverseRewind()
    {
        if (rewindIndex > 0)
        {
            StateInTime stateInTime = statesInTime[rewindIndex];

            rigidbodyRef.MovePosition(stateInTime.position + stateInTime.linearVelocity * Time.fixedDeltaTime);
            Quaternion deltaRotation = Quaternion.Euler(stateInTime.angularVelocity * Time.fixedDeltaTime);
            rigidbodyRef.MoveRotation(stateInTime.rotation * deltaRotation);

            rewindIndex--;
        }
        else
        {
            SetState(TimeBodyStates.Natural);

        }
    }

    private void onEnterRewind()
    {
        rigidbodyRef.isKinematic = true;
    }

    private void onEnterFreeze()
    {
        rigidbodyRef.isKinematic = true;
        rigidbodyRef.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnExitRewind()
    {
        rigidbodyRef.isKinematic = false;
        SetPreviousPhysicalState();
    }
    private void OnExitFreeze()
    {
        rigidbodyRef.isKinematic = false;
        rigidbodyRef.constraints = 0;
        SetPreviousPhysicalState();
    }
}