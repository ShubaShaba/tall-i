using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonCarriableTimeObj : MovingPlatformBase, ITimeBody
{
    [SerializeField] private ControlManager controlManager;
    [SerializeField] private float rewindTimeTime = 15;
    [SerializeField] private int slowDownCoefficient = 2;
    private TimeBendingVisual visuals;
    private PhysicalTimeBendingController timeBendingController;
    private PhysicalTimeObj physicalTimeObjBase;

    private void Start()
    {
        visuals = GetComponent<TimeBendingVisual>();
        timeBendingController = new PhysicalTimeBendingController(rewindTimeTime, transform, rb, slowDownCoefficient);
        physicalTimeObjBase = new PhysicalTimeObj(visuals, timeBendingController);

        foreach (TimeBodyStates state in Enum.GetValues(typeof(TimeBodyStates)))
        {
            if (state != TimeBodyStates.Natural) {
                timeBendingController.AddDuringStateActionFixedUpdate(state, DuringAnyTimeStateExceptNatural);
                timeBendingController.AddOnEnterAction(state, OnAnyStateEnterExceptNatural);
            }
            timeBendingController.AddOnEnterAction(state, OnAnyStateEnter);
            timeBendingController.AddOnExitAction(state, OnAnyStateExit);
        }
    }

    void FixedUpdate()
    {
        MoveInFixedUpdate();
        timeBendingController.HandleTime();
    }

    public void Focus()
    {
        physicalTimeObjBase.Focus();
    }

    public void UnFocus()
    {
        physicalTimeObjBase.UnFocus();
    }

    public bool IsInManualMode()
    {
        return physicalTimeObjBase.IsInManualMode();
    }

    public void ManualBackward()
    {
        physicalTimeObjBase.ManualBackward();
    }

    public void ManualForward()
    {
        physicalTimeObjBase.ManualForward();
    }

    public void ToggleFreeze()
    {
        physicalTimeObjBase.ToggleState(TimeBodyStates.Stoped);
    }

    public void ToggleManualControl()
    {
        physicalTimeObjBase.ToggleState(TimeBodyStates.ControlledStoped);
    }

    public void ToggleRewind()
    {
        physicalTimeObjBase.ToggleState(TimeBodyStates.Rewinding);
    }

    private void DuringAnyTimeStateExceptNatural()
    {
        UpdateTarget();
    }

    private void OnAnyStateEnterExceptNatural()
    {
        StopCycling();
    }

    private void OnAnyStateEnter()
    {
        rb.isKinematic = true;
    }

    private void OnAnyStateExit()
    {
        StartCycling();
    }
}