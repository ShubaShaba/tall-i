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
    [SerializeField] private Vector3 deathZone;
    [SerializeField] private Transform respawn;
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
            if (state != TimeBodyStates.Natural)
                timeBendingController.AddDuringStateActionFixedUpdate(state, DuringAnyStateExceptNatural);
        }
    }

    void FixedUpdate() { timeBendingController.HandleTime(); }

    public void Focus() { physicalTimeObjBase.Focus(); }

    public void UnFocus() { physicalTimeObjBase.UnFocus(); }

    public void ToggleRewind()
    {
        Throw(0);
        physicalTimeObjBase.ToggleState(TimeBodyStates.Rewinding);
    }

    public void ToggleFreeze()
    {
        Throw(0);
        physicalTimeObjBase.ToggleState(TimeBodyStates.Stoped);
    }

    public void ToggleManualControl()
    {
        Throw(0);
        physicalTimeObjBase.ToggleState(TimeBodyStates.ControlledStoped);
    }

    public void ManualBackward() { physicalTimeObjBase.ManualBackward(); }

    public void ManualForward() { physicalTimeObjBase.ManualForward(); }

    public bool IsInManualMode() { return physicalTimeObjBase.IsInManualMode(); }

    protected override void OnPickup() { timeBendingController.ForceQuite(); }

    private void DuringAnyStateExceptNatural()
    {
        Collider[] hitColliders = Physics.OverlapBox(rb.position, deathZone, transform.rotation);
        if (hitColliders.Length > 1)
        {
            visuals.RespawnAnimation(true);
            timeBendingController.HardReset();
            transform.position = respawn.position;
            visuals.RespawnAnimation(false);
        }
    }
}