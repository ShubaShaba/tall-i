using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarriableTimeObj : CarriableBase, ITimeBody, IRespawnable
{
    [SerializeField] private float rewindTimeTime = 5;
    [SerializeField] private int slowDownCoefficient = 2;
    [SerializeField] private float deathZoneSensitivity = 0.6f;
    [SerializeField] private Transform respawn;
    [SerializeField] private bool recordOnlyInMotion = false;
    [SerializeField] private bool resetOnPickUp = true;
    private Collider deathZone;
    private TimeBendingVisual visuals;
    private PhysicalTimeBendingEntityController timeBendingController;
    private PhysicalTimeBodySound physicalTimeBodySound;


    private void Start()
    {
        visuals = GetComponent<TimeBendingVisual>();
        physicalTimeBodySound = new PhysicalTimeBodySound(transform);
        timeBendingController = new PhysicalTimeBendingEntityController(
            rewindTimeTime, transform, rb, visuals, physicalTimeBodySound, slowDownCoefficient);
        deathZone = GetComponent<Collider>();

        foreach (TimeBodyStates state in Enum.GetValues(typeof(TimeBodyStates)))
        {
            if (state != TimeBodyStates.Natural)
                timeBendingController.AddDuringStateActionFixedUpdate(state, DuringAnyStateExceptNatural);
        }

        if (recordOnlyInMotion)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            timeBendingController.SetRecordConstraints(() => !rb.IsSleeping());
        }

    }

    void FixedUpdate() { timeBendingController.HandleTime(); }

    public void Focus() { timeBendingController.Focus(); }

    public void UnFocus() { timeBendingController.UnFocus(); }

    public void ToggleRewind()
    {
        Throw(Vector3.zero, false);
        timeBendingController.ToggleState(TimeBodyStates.Rewinding);
    }

    public void ToggleFreeze()
    {
        Throw(Vector3.zero, false);
        timeBendingController.ToggleState(TimeBodyStates.Stoped);
    }

    public void ToggleManualControl()
    {
        Throw(Vector3.zero, false);
        timeBendingController.ToggleState(TimeBodyStates.ControlledStoped);
    }

    public void ManualBackward() { timeBendingController.ManualBackward(); }

    public void ManualForward() { timeBendingController.ManualForward(); }

    public bool IsInManualMode() { return timeBendingController.IsInManualMode(); }

    protected override void OnPickup()
    {
        timeBendingController.ForceQuite();
        if (resetOnPickUp) timeBendingController.Reset();
    }

    private void DuringAnyStateExceptNatural()
    {
        Collider[] hitColliders = DeathZone.GetIntesectingColliders(rb.position, transform.rotation, deathZone, deathZoneSensitivity);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (!hitColliders[i].isTrigger && hitColliders[i] != GetComponent<Collider>())
            {
                Respawn();
                return;
            }
        }
    }

    public TimeBodyStates GetCurrentState() { return timeBendingController.GetCurrentState(); }

    public void Respawn()
    {
        visuals.RespawnAnimation(true);
        timeBendingController.Reset();
        transform.position = respawn.position;
        visuals.RespawnAnimation(false);
        // visuals.FocusAnimation();
        physicalTimeBodySound.HardResetSound();
    }
}