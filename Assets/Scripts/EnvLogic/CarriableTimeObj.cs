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
    [SerializeField] private float deathZoneSensitivity = 0.6f;
    [SerializeField] private Transform respawn;
    private Collider deathZone;
    private TimeBendingVisual visuals;
    private PhysicalTimeBendingController timeBendingController;
    private PhysicalTimeHelper physicalTimeObjHelper;
    private PhysicalTimeBodySound physicalTimeBodySound;
   

    private void Start()
    {
        visuals = GetComponent<TimeBendingVisual>();
        timeBendingController = new PhysicalTimeBendingController(rewindTimeTime, transform, rb, slowDownCoefficient);
        physicalTimeObjHelper = new PhysicalTimeHelper(visuals, timeBendingController);
        deathZone = GetComponent<Collider>();

        foreach (TimeBodyStates state in Enum.GetValues(typeof(TimeBodyStates)))
        {
            if (state != TimeBodyStates.Natural)
                timeBendingController.AddDuringStateActionFixedUpdate(state, DuringAnyStateExceptNatural);
        }

        physicalTimeBodySound = new PhysicalTimeBodySound(timeBendingController, transform);
    }

    void FixedUpdate() { timeBendingController.HandleTime(); }

    public void Focus() { physicalTimeObjHelper.Focus(); }

    public void UnFocus() { physicalTimeObjHelper.UnFocus(); }

    public void ToggleRewind()
    {
        Throw(Vector3.zero, false);
        physicalTimeObjHelper.ToggleState(TimeBodyStates.Rewinding);
    }

    public void ToggleFreeze()
    {
        Throw(Vector3.zero, false);
        physicalTimeObjHelper.ToggleState(TimeBodyStates.Stoped);
    }

    public void ToggleManualControl()
    {
        Throw(Vector3.zero, false);
        physicalTimeObjHelper.ToggleState(TimeBodyStates.ControlledStoped);
    }

    public void ManualBackward() { physicalTimeObjHelper.ManualBackward(); }

    public void ManualForward() { physicalTimeObjHelper.ManualForward(); }

    public bool IsInManualMode() { return physicalTimeObjHelper.IsInManualMode(); }

    protected override void OnPickup() { timeBendingController.ForceQuite(); }

    private void DuringAnyStateExceptNatural()
    {
        Collider[] hitColliders = DeathZone.GetIntesectingColliders(rb.position, transform.rotation, deathZone, deathZoneSensitivity);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (!hitColliders[i].isTrigger && hitColliders[i] != GetComponent<Collider>())
            {
                Debug.Log(hitColliders[i].name);
                visuals.RespawnAnimation(true);
                timeBendingController.HardReset();
                transform.position = respawn.position;
                visuals.RespawnAnimation(false);
                visuals.FocusAnimation();
                return;
            }
        }
    }

    public TimeBodyStates GetCurrentState() { return timeBendingController.GetCurrentState(); }
}