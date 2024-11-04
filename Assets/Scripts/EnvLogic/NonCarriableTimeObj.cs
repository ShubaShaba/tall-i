using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonCarriableTimeObj : MonoBehaviour, ITimeBody
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    
    public void Focus()
    {
        // throw new System.NotImplementedException();
    }

    public void UnFocus()
    {
        throw new System.NotImplementedException();
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