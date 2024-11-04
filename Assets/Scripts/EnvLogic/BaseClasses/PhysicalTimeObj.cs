using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Helper class for other physical time objects
public class PhysicalTimeObj
{
    private TimeBendingVisual visuals;
    private PhysicalTimeBendingController timeBendingController;

    public PhysicalTimeObj(TimeBendingVisual _visuals, PhysicalTimeBendingController _timeBendingController)
    {
        visuals = _visuals;
        timeBendingController = _timeBendingController;
        visuals.InitializeVisuals(timeBendingController);
    }

    public void Focus()
    {
        visuals.FocusAnimation();
    }

    public void UnFocus()
    {
        CancelTimeTimeBendingAction();
        visuals.CancelEverything();
    }

    public void ToggleState(TimeBodyStates state)
    {
        if (timeBendingController.GetCurrentState() == state)
        {
            CancelTimeTimeBendingAction();
            return;
        }
        timeBendingController.SetState(state);
    }

    public void ManualBackward()
    {
        if (!(timeBendingController.GetCurrentState() == TimeBodyStates.ControlledStoped)) return;
        timeBendingController.SetState(TimeBodyStates.ControlledRewinding);
    }

    public void ManualForward()
    {
        if (!(timeBendingController.GetCurrentState() == TimeBodyStates.ControlledStoped)) return;
        timeBendingController.SetState(TimeBodyStates.ControlledReverseRewinding);
    }

    public bool IsInManualMode()
    {
        return
            timeBendingController.GetCurrentState() == TimeBodyStates.ControlledRewinding ||
            timeBendingController.GetCurrentState() == TimeBodyStates.ControlledReverseRewinding;
    }

    public void CancelTimeTimeBendingAction()
    {
        if (timeBendingController.GetPreviousState() == TimeBodyStates.Rewinding && timeBendingController.GetCurrentState() == TimeBodyStates.Stoped)
        {
            timeBendingController.SetState(TimeBodyStates.Rewinding);
        }
        else
        {
            timeBendingController.SetState(TimeBodyStates.Natural);
        }
    }
}