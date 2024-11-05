using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PhysicalTimeBendingController : StateMachine
{
    private List<StateInTime> statesInTime;
    private Transform transformRef;
    private Rigidbody rigidbodyRef;
    private float rememberTime;
    private int slowDownC;
    private Func<bool> recordConstraints;

    private int rewindIndex = 0;
    private int slowDownIndex = 0;

    public PhysicalTimeBendingController(
        float _rememberTime,
        Transform _transformRef,
        Rigidbody _rigidbodyRef,
        int _slowDownC = 1
    )
    {
        Init();
        statesInTime = new List<StateInTime>();
        rememberTime = _rememberTime;
        transformRef = _transformRef;
        rigidbodyRef = _rigidbodyRef;
        slowDownC = _slowDownC;
        recordConstraints = () => true;

        AddOnEnterAction(TimeBodyStates.Rewinding, onEnterRewind);
        AddOnEnterAction(TimeBodyStates.ReverseRewinding, onEnterRewind);
        AddOnEnterAction(TimeBodyStates.Stoped, onEnterFreeze);
        AddOnExitAction(TimeBodyStates.Rewinding, OnExitRewind);
        AddOnExitAction(TimeBodyStates.ReverseRewinding, OnExitRewind);
        AddOnExitAction(TimeBodyStates.Stoped, OnExitFreeze);

        AddOnEnterAction(TimeBodyStates.ControlledRewinding, onEnterRewind);
        AddOnEnterAction(TimeBodyStates.ControlledReverseRewinding, onEnterRewind);
        AddOnEnterAction(TimeBodyStates.ControlledStoped, onEnterFreeze);
        AddOnExitAction(TimeBodyStates.ControlledRewinding, OnExitRewind);
        AddOnExitAction(TimeBodyStates.ControlledReverseRewinding, OnExitRewind);
        AddOnExitAction(TimeBodyStates.ControlledStoped, OnExitFreeze);
    }

    // WARNING TO BE EXECUTED IN FixedUpdate ONLY !!!
    public void HandleTime()
    {
        switch (currentState)
        {
            case TimeBodyStates.Natural:
                if (rewindIndex > 0)
                {
                    SetState(TimeBodyStates.ReverseRewinding);
                }
                else if (recordConstraints())
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
            case TimeBodyStates.ControlledRewinding:
                Rewind(slowDownC);
                break;
            case TimeBodyStates.ControlledReverseRewinding:
                ReverseRewind(slowDownC);
                break;
            default:
                break;
        }
        ExecuteActions(duringStateActions);
    }

    public void ForceQuite()
    {
        for (int i = 0; i < rewindIndex; i++)
        {
            statesInTime.RemoveAt(0);
        }
        rewindIndex = 0;
        slowDownIndex = 0;
        SetState(TimeBodyStates.Natural);
    }

    public void HardReset()
    {
        statesInTime.Clear();
        slowDownIndex = 0;
        rewindIndex = 0;
        SetState(TimeBodyStates.Natural);
        rigidbodyRef.velocity = Vector3.zero;
        rigidbodyRef.rotation = Quaternion.identity;
        transformRef.rotation = Quaternion.identity;
    }

    public StateInTime GetCurrentStateInTime() { return statesInTime[rewindIndex]; }

    public void SetRecordConstraints(Func<bool> action) { recordConstraints = action; }

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

    private void Rewind(int slowDownCoefficient = 1)
    {
        if (rewindIndex < statesInTime.Count)
        {
            StateInTime stateInTime = statesInTime[rewindIndex];
            Vector3 direction = stateInTime.position - transformRef.position;
            rigidbodyRef.MovePosition(transformRef.position + (direction / slowDownCoefficient * (slowDownIndex + 1)));
            rigidbodyRef.MoveRotation(Quaternion.Slerp(transformRef.rotation, stateInTime.rotation, 1f / slowDownCoefficient));

            slowDownIndex++;
            if (slowDownIndex >= slowDownCoefficient)
            {
                rewindIndex++;
                slowDownIndex = 0;
            }
        }
        else
        {
            statesInTime.Clear();
            rewindIndex = 0;
            SetState(TimeBodyStates.Natural);
        }
    }

    private void ReverseRewind(int slowDownCoefficient = 1)
    {
        if (rewindIndex > 0)
        {
            StateInTime stateInTime = statesInTime[rewindIndex];
            Vector3 direction = stateInTime.position - transformRef.position;
            rigidbodyRef.MovePosition(transformRef.position + (direction / slowDownCoefficient * (slowDownIndex + 1)));
            rigidbodyRef.MoveRotation(Quaternion.Slerp(transformRef.rotation, stateInTime.rotation, 1f / slowDownCoefficient));

            slowDownIndex++;
            if (slowDownIndex >= slowDownCoefficient)
            {
                rewindIndex--;
                slowDownIndex = 0;
            }
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
        rigidbodyRef.constraints = RigidbodyConstraints.FreezeAll;
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