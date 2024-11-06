using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerUI
{
    public bool isCarryingSomething();
    public bool isFocusedOnSomethingType1();
    public bool isFocusedOnSomethingType2();
    public TimeBodyStates GetCurrentFocusState();
}
