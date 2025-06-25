using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Helper class for other physical time objects
public class PhysicalTimeBendingEntityController : PhysicalTimeBendingController
{
    private TimeBendingVisual visuals;
    private PhysicalTimeBodySound sound;

    public PhysicalTimeBendingEntityController(
        float _rememberTime,
        Transform _transformRef,
        Rigidbody _rigidbodyRef,
        TimeBendingVisual _visuals,
        PhysicalTimeBodySound _sound,
        int _slowDownC = 1
    ) : base(_rememberTime, _transformRef, _rigidbodyRef, _slowDownC)
    {
        visuals = _visuals;
        visuals.ConnectVisuals(this);
        sound = _sound;
        sound.ConnectSound(this);
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
        if (GetCurrentState() == state)
        {
            CancelTimeTimeBendingAction();
            return;
        }
        SetState(state);
    }

    public void ManualBackward()
    {
        if (!(GetCurrentState() == TimeBodyStates.ControlledStoped)) return;
        SetState(TimeBodyStates.ControlledRewinding);
    }

    public void ManualForward()
    {
        if (!(GetCurrentState() == TimeBodyStates.ControlledStoped)) return;
        SetState(TimeBodyStates.ControlledReverseRewinding);
    }

    public bool IsInManualMode()
    {
        return
            GetCurrentState() == TimeBodyStates.ControlledRewinding ||
            GetCurrentState() == TimeBodyStates.ControlledReverseRewinding;
    }

    public void CancelTimeTimeBendingAction()
    {
        if (GetPreviousState() == TimeBodyStates.Rewinding && GetCurrentState() == TimeBodyStates.Stoped)
        {
            SetState(TimeBodyStates.Rewinding);
        }
        else
        {
            SetState(TimeBodyStates.Natural);
        }
    }
}