using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBendingVisual : MonoBehaviour
{
    [SerializeField] private ParticleSystem focusing;
    [SerializeField] private ParticleSystem rewinding;
    [SerializeField] private ParticleSystem freeze;
    [SerializeField] private ParticleSystem controlledFreeze;
    [SerializeField] private ParticleSystem controlledRewiding;


    private void Awake()
    {
        CancelEverything();
    }

    public void CancelEverything()
    {
        focusing.Stop();
        rewinding.Stop();
        freeze.Stop();
        controlledFreeze.Stop();
        controlledRewiding.Stop();
        focusing.Clear();
        rewinding.Clear();
        freeze.Clear();
        controlledFreeze.Clear();
        controlledRewiding.Clear();
    }

    public void FocusAnimation()
    {
        CancelEverything();
        focusing.Play();
    }

    public void RewindAnimation()
    {
        CancelEverything();
        rewinding.Play();
    }

    public void FreezeAnimation()
    {
        CancelEverything();
        freeze.Play();
    }

    public void ControlledFreezeAnimation()
    {
        CancelEverything();
        controlledFreeze.Play();
    }

    public void ControlledRewindAnimation()
    {
        CancelEverything();
        controlledRewiding.Play();
    }
}
