using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformTimeObj : MovingPlatformBase, ITimeBody
{
    [SerializeField] private ControlManager controlManager;
    [SerializeField] private float rewindTimeTime = 15;
    [SerializeField] private int slowDownCoefficient = 2;
    [SerializeField] private float deathZoneSensitivity = 0.95f;
    private Collider deathZone;
    private TimeBendingVisual visuals;
    private PhysicalTimeBendingController timeBendingController;
    private PhysicalTimeHelper physicalTimeObjHelper;
    private PhysicalTimeBodySound physicalTimeBodySound;
    private bool deathZoneStop;

    private void Start()
    {
        visuals = GetComponent<TimeBendingVisual>();
        timeBendingController = new PhysicalTimeBendingController(rewindTimeTime, transform, rb, slowDownCoefficient);
        physicalTimeObjHelper = new PhysicalTimeHelper(visuals, timeBendingController);
        deathZone = GetComponent<Collider>();

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

        deathZoneStop = false;
        // timeBendingController.SetRecordConstraints(RecordConstraints);

        physicalTimeBodySound = new PhysicalTimeBodySound(timeBendingController, transform);
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
        Collider[] hitColliders = DeathZone.GetIntesectingColliders(
            rb.position, transform.rotation, deathZone, deathZoneSensitivity);
        
        int counter = 0;
        for (int i = 0; i < hitColliders.Length; i++)
            if (!hitColliders[i].isTrigger && hitColliders[i] != GetComponent<Collider>()) counter++;

        if (counter > 0)
        {
            StopCycling();
            deathZoneStop = true;
        }
        else if (deathZoneStop)
        {
            StartCycling();
            deathZoneStop = false;
        }
    }

    private void OnAnyStateEnterExceptNatural() { StopCycling(); }

    private void OnAnyStateEnter() { rb.isKinematic = true; }

    private void OnAnyStateExit() { StartCycling(); }

    public TimeBodyStates GetCurrentState() { return timeBendingController.GetCurrentState(); }

    // private bool RecordConstraints() { return isMoving; }
}