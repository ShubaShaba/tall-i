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
    private PhysicalTimeBendingEntityController timeBendingController;
    private PhysicalTimeBodySound physicalTimeBodySound;
    private bool deathZoneStop;

    private void Start()
    {
        visuals = GetComponent<TimeBendingVisual>();
        physicalTimeBodySound = new PhysicalTimeBodySound(transform);
        timeBendingController = new PhysicalTimeBendingEntityController(
            rewindTimeTime, transform, rb, visuals, physicalTimeBodySound, slowDownCoefficient);
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
    }

    void FixedUpdate()
    {
        timeBendingController.HandleTime();
        MoveInFixedUpdate();
    }

    public void Focus() { timeBendingController.Focus(); }

    public void UnFocus() { timeBendingController.UnFocus(); }

    public bool IsInManualMode() { return timeBendingController.IsInManualMode(); }

    public void ManualBackward() { timeBendingController.ManualBackward(); }

    public void ManualForward() { timeBendingController.ManualForward(); }

    public void ToggleFreeze() { timeBendingController.ToggleState(TimeBodyStates.Stoped); }

    public void ToggleManualControl() { timeBendingController.ToggleState(TimeBodyStates.ControlledStoped); }

    public void ToggleRewind() { timeBendingController.ToggleState(TimeBodyStates.Rewinding); }

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