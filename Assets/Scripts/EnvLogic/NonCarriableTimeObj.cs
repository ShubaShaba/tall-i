using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonCarriableTimeObj : MonoBehaviour, ITimeBody
{
    [SerializeField] private ControlManager controlManager;
    [SerializeField] private float rewindTimeTime = 15;
    [SerializeField] private int slowDownCoefficient = 2;
    private TimeBendingVisual visuals;
    private PhysicalTimeBendingController timeBendingController;
    private Rigidbody rb;
    private PhysicalTimeObj physicalTimeObjBase;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        visuals = GetComponent<TimeBendingVisual>();
        timeBendingController = new PhysicalTimeBendingController(rewindTimeTime, transform, rb, slowDownCoefficient);
        physicalTimeObjBase = new PhysicalTimeObj(visuals, timeBendingController);
    }

    void FixedUpdate()
    {
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
        throw new System.NotImplementedException();
    }

    public void ManualBackward()
    {
        throw new System.NotImplementedException();
    }

    public void ManualForward()
    {
        throw new System.NotImplementedException();
    }

    public void ToggleFreeze()
    {
        throw new System.NotImplementedException();
    }

    public void ToggleManualControl()
    {
        throw new System.NotImplementedException();
    }

    public void ToggleRewind()
    {
        throw new System.NotImplementedException();
    }
}