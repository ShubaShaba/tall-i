using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarriableTimeObj : CarriableBase, ITimeBody
{
    [SerializeField] private ControlManager controlManager;
    [SerializeField] private float rewindTimeTime = 5;
    [SerializeField] private int slowDownCoefficient = 2;
    private TimeBendingVisual visuals;
    private PhysicalTimeBendingController timeBendingController;

    private void Start()
    {
        visuals = GetComponent<TimeBendingVisual>();
        timeBendingController = new PhysicalTimeBendingController(rewindTimeTime, transform, rb, slowDownCoefficient);
        timeBendingController.AddOnEnterAction(TimeBodyStates.Rewinding, onEnterRewind);
        timeBendingController.AddOnEnterAction(TimeBodyStates.Stoped, onEnterFreeze);
        timeBendingController.AddOnExitAction(TimeBodyStates.Rewinding, OnExit);
        timeBendingController.AddOnExitAction(TimeBodyStates.Stoped, OnExit);

        timeBendingController.AddOnEnterAction(TimeBodyStates.ControlledRewinding, onEnterControlledRewind);
        timeBendingController.AddOnEnterAction(TimeBodyStates.ControlledReverseRewinding, onEnterControlledRewind);
        timeBendingController.AddOnEnterAction(TimeBodyStates.ControlledStoped, onEnterControlledFreeze);
        timeBendingController.AddOnExitAction(TimeBodyStates.ControlledStoped, OnExit);
        timeBendingController.AddOnExitAction(TimeBodyStates.ControlledRewinding, OnExit);
        timeBendingController.AddOnExitAction(TimeBodyStates.ControlledReverseRewinding, OnExit);
    }

    void FixedUpdate()
    {
        timeBendingController.HandleTime();
    }

    public void Focus()
    {
        visuals.FocusAnimation();
    }

    public void UnFocus()
    {
        CancelTimeTimeBendingAction();
        visuals.CancelEverything();
    }

    public void ToggleRewind()
    {
        Throw(0);
        if (timeBendingController.GetCurrentState() == TimeBodyStates.Rewinding)
        {
            CancelTimeTimeBendingAction();
            return;
        }
        timeBendingController.SetState(TimeBodyStates.Rewinding);
    }


    public void ToggleFreeze()
    {
        Throw(0);
        if (timeBendingController.GetCurrentState() == TimeBodyStates.Stoped)
        {
            CancelTimeTimeBendingAction();
            return;
        }
        timeBendingController.SetState(TimeBodyStates.Stoped);
    }

    public void ToggleManualControl()
    {
        Throw(0);
        if (timeBendingController.GetCurrentState() == TimeBodyStates.ControlledStoped)
        {
            CancelTimeTimeBendingAction();
            return;
        }
        timeBendingController.SetState(TimeBodyStates.ControlledStoped);
    }

    public void ManualBackward()
    {
        if (!(timeBendingController.GetCurrentState() == TimeBodyStates.ControlledStoped)) return;
        timeBendingController.SetState(TimeBodyStates.ControlledRewinding);
    }

    public void ManualForward()
    {
        if (!(timeBendingController.GetCurrentState() == TimeBodyStates.ControlledStoped)) return;
        timeBendingController.SetState(TimeBodyStates.ControlledReverseRewinding);
    }

    public bool IsInManualMode()
    {
        return 
            timeBendingController.GetCurrentState() == TimeBodyStates.ControlledRewinding ||
            timeBendingController.GetCurrentState() == TimeBodyStates.ControlledReverseRewinding;
    }

    public override bool CanPick()
    {
        return timeBendingController.GetCurrentState() == TimeBodyStates.Natural;
    }

    private void CancelTimeTimeBendingAction()
    {
        if (timeBendingController.GetPreviousState() == TimeBodyStates.Rewinding && timeBendingController.GetCurrentState() == TimeBodyStates.Stoped)
        {
            timeBendingController.SetState(TimeBodyStates.Rewinding);
        }
        else
        {
            timeBendingController.SetState(TimeBodyStates.Natural);
        }
    }

    private void onEnterRewind()
    {
        visuals.RewindAnimation();
    }

    private void onEnterFreeze()
    {
        visuals.FreezeAnimation();
    }

    private void onEnterControlledRewind()
    {
        visuals.ControlledRewindAnimation();
    }

    private void onEnterControlledFreeze()
    {
        visuals.ControlledFreezeAnimation();
    }

    private void OnExit()
    {
        visuals.FocusAnimation();
    }
}