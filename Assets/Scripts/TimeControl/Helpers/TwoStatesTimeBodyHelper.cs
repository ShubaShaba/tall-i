using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoStatesTimeBodyHelper
{
    private TimeBendingVisual visuals;
    private TwoStatesTimeBendingController timeBendingController;

    public TwoStatesTimeBodyHelper(TimeBendingVisual _visuals, TwoStatesTimeBendingController _timeBendingController)
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
        visuals.CancelEverything();
    }

    public void ToggleState()
    {
        if (timeBendingController.GetCurrentState() == TimeBodyStates.Natural)
        {
            timeBendingController.SetState(TimeBodyStates.Rewinding);
        }
        else
        {
            timeBendingController.SetState(TimeBodyStates.Natural);
        }
    }
 
}
