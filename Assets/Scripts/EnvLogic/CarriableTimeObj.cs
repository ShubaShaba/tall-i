using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarriableTimeObj : CarriableBase, ITimeBody
{
    [SerializeField] private ControlManager controlManager;
    private TimeBendingVisual visuals;
    private TimeBendingController timeBendingController;

    public void Focus()
    {
        visuals.FocusAnimation();
    }

    public void UnFocus()
    {
        CancelTimeTimeBendingAction();
        visuals.CancelEverything();
    }

    public void StartRewinding()
    {
        timeBendingController.SetState(TimeBodyStates.Rewinding);
    }


    public void StartFreezing()
    {
        timeBendingController.SetState(TimeBodyStates.Stoped);
    }

    public void CancelTimeTimeBendingAction()
    {
        if (timeBendingController.GetPreviousState() == TimeBodyStates.Rewinding && timeBendingController.GetCurrentState() == TimeBodyStates.Stoped)
        {
            Debug.Log("AAAAAAAAAAAAA");
            timeBendingController.SetState(TimeBodyStates.Rewinding);
        }
        else
        {
            timeBendingController.SetState(TimeBodyStates.Natural);
        }
    }

    public override bool CanPick()
    {
        return timeBendingController.GetCurrentState() == TimeBodyStates.Natural;
    }

    private void Start()
    {
        visuals = GetComponent<TimeBendingVisual>();
        timeBendingController = new TimeBendingController(5, transform, rb);
        timeBendingController.AddOnEnterAction(TimeBodyStates.Rewinding, onEnterRewind);
        timeBendingController.AddOnEnterAction(TimeBodyStates.Stoped, onEnterFreeze);
        timeBendingController.AddOnExitAction(TimeBodyStates.Rewinding, OnExitRewind);
        timeBendingController.AddOnExitAction(TimeBodyStates.Stoped, OnExitFreeze);
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
        visuals.FocusAnimation();
    }
    private void OnExitFreeze()
    {
        rb.isKinematic = false;
        rb.constraints = 0;
        visuals.FocusAnimation();
    }

    void FixedUpdate()
    {
        timeBendingController.HandleTime();
    }
}