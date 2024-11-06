using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : KeyHole, ITimeBody
{
    [SerializeField] private ControlManager controlManager;
    private TimeBendingVisual visuals;
    private TwoStatesTimeBendingController timeBendingController;
    private TwoStatesTimeBodyHelper twoStatesTimeBodyHelper;

    private void Start()
    {
        visuals = GetComponent<TimeBendingVisual>();
        timeBendingController = new TwoStatesTimeBendingController();
        twoStatesTimeBodyHelper = new TwoStatesTimeBodyHelper(visuals, timeBendingController);

        timeBendingController.AddOnEnterAction(TimeBodyStates.Rewinding, OnEnterRewind);
        timeBendingController.AddOnEnterAction(TimeBodyStates.Natural, OnEnterNatural);
    }

    public void Focus() { twoStatesTimeBodyHelper.Focus(); }

    public void UnFocus() { twoStatesTimeBodyHelper.UnFocus(); }

    public void ToggleRewind() { twoStatesTimeBodyHelper.ToggleState(); }

    public override void Interact()
    {
        if (isInjected() && !isTriggered && timeBendingController.GetCurrentState() == TimeBodyStates.Natural)
        {
            Switch();
            isTriggered = true;
        }
    }

    public override void Eject() { if (!isTriggered) base.Eject(); }
    public void ManualBackward() { return; }
    public void ManualForward() { return; }
    public void ToggleFreeze() { return; }
    public void ToggleManualControl() { return; }
    public bool IsInManualMode() { return false; }

    private void delayedSwitch()
    {
        if (isTriggered)
        {
            Switch();
            isTriggered = false;
        }
        ToggleRewind();
    }

    private void OnEnterRewind()
    {
        visuals.RewindAnimation();
        Invoke("delayedSwitch", 2f);
    }
    private void OnEnterNatural()
    {
        CancelInvoke("delayedSwitch");
        visuals.FocusAnimation();
    }

    public TimeBodyStates GetCurrentState()
    {
        return timeBendingController.GetCurrentState();
    }
}