using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformTimeObj : MovingPlatformBase, ITimeBody
{
    [SerializeField] private ControlManager controlManager;
    [SerializeField] private float rewindTimeTime = 15;
    [SerializeField] private int slowDownCoefficient = 2;
    [SerializeField] private Vector3 deathZone;
    private TimeBendingVisual visuals;
    private PhysicalTimeBendingController timeBendingController;
    private PhysicalTimeHelper physicalTimeObjHelper;

    private void Start()
    {
        visuals = GetComponent<TimeBendingVisual>();
        timeBendingController = new PhysicalTimeBendingController(rewindTimeTime, transform, rb, slowDownCoefficient);
        physicalTimeObjHelper = new PhysicalTimeHelper(visuals, timeBendingController);

        timeBendingController.AddDuringStateActionFixedUpdate(TimeBodyStates.Natural, DuringNaturalState);

        foreach (TimeBodyStates state in Enum.GetValues(typeof(TimeBodyStates)))
        {
            if (state != TimeBodyStates.Natural)
            {
                timeBendingController.AddOnEnterAction(state, OnAnyStateEnterExceptNatural);
            }
            timeBendingController.AddOnEnterAction(state, OnAnyStateEnter);
            timeBendingController.AddOnExitAction(state, OnAnyStateExit);
        }

        // timeBendingController.SetRecordConstraints(RecordConstraints);
    }

    void FixedUpdate()
    {
        timeBendingController.HandleTime();
        MoveInFixedUpdate();
    }

    public void Focus() { physicalTimeObjHelper.Focus(); }

    public void UnFocus() { physicalTimeObjHelper.UnFocus(); }

    public bool IsInManualMode() { return physicalTimeObjHelper.IsInManualMode(); }

    public void ManualBackward() { physicalTimeObjHelper.ManualBackward(); }

    public void ManualForward() { physicalTimeObjHelper.ManualForward(); }

    public void ToggleFreeze() { physicalTimeObjHelper.ToggleState(TimeBodyStates.Stoped); }

    public void ToggleManualControl() { physicalTimeObjHelper.ToggleState(TimeBodyStates.ControlledStoped); }

    public void ToggleRewind() { physicalTimeObjHelper.ToggleState(TimeBodyStates.Rewinding); }

    private void DuringNaturalState()
    {
        Collider[] hitColliders = Physics.OverlapBox(rb.position, deathZone, transform.rotation);
        if (hitColliders.Length > 1)
        {
            StopCycling();
        }
        else
        {
            StartCycling();
        }
    }

    private void OnAnyStateEnterExceptNatural() { StopCycling(); }

    private void OnAnyStateEnter() { rb.isKinematic = true; }

    private void OnAnyStateExit() { StartCycling(); }

    // private bool RecordConstraints() { return isMoving; }
}