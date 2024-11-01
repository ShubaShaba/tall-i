using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeBody
{
    public void Focus();
    public void UnFocus();
    public void StartRewinding();
    public void StartFreezing();
    public void CancelTimeTimeBendingAction();
    
}
