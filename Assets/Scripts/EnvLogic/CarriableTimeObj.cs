using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarriableTimeObj : CarriableBase, ITimeBody
{
    [SerializeField] private ControlManager controlManager;
    [SerializeField] private float rewindTimeTime = 5;
    private TimeBendingVisual visuals;
    private PhysicalTimeBendingController timeBendingController;

    private void Start()
    {
        visuals = GetComponent<TimeBendingVisual>();
        timeBendingController = new PhysicalTimeBendingController(rewindTimeTime, transform, rb);
        timeBendingController.AddOnEnterAction(TimeBodyStates.Rewinding, onEnterRewind);
        timeBendingController.AddOnEnterAction(TimeBodyStates.Stoped, onEnterFreeze);
        timeBendingController.AddOnExitAction(TimeBodyStates.Rewinding, OnExitRewind);
        timeBendingController.AddOnExitAction(TimeBodyStates.Stoped, OnExitFreeze);
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
        if (timeBendingController.GetCurrentState() == TimeBodyStates.Rewinding)
        {
            CancelTimeTimeBendingAction();
            return;
        }
        timeBendingController.SetState(TimeBodyStates.Rewinding);
    }


    public void ToggleFreeze()
    {
        if (timeBendingController.GetCurrentState() == TimeBodyStates.Stoped)
        {
            CancelTimeTimeBendingAction();
            return;
        }
        timeBendingController.SetState(TimeBodyStates.Stoped);
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
        rb.isKinematic = true;
        visuals.RewindAnimation();
    }

    private void onEnterFreeze()
    {
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        visuals.FreezeAnimation();
    }

    private void OnExitRewind()
    {
        rb.isKinematic = false;
        timeBendingController.setPreviousPhysicalState();
        visuals.FocusAnimation();
    }
    private void OnExitFreeze()
    {
        rb.isKinematic = false;
        rb.constraints = 0;
        timeBendingController.setPreviousPhysicalState();
        visuals.FocusAnimation();
    }
}