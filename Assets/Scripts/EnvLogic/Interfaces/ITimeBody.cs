using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeBody
{
    public void Focus();
    public void UnFocus();
    public void ToggleRewind();
    public void ToggleFreeze();    
}
