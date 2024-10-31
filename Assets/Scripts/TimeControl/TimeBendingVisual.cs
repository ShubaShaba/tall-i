using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBendingVisual : MonoBehaviour
{
    [SerializeField] private ParticleSystem focusing;
    [SerializeField] private ParticleSystem rewinding;
    [SerializeField] private ParticleSystem freeze;

    private void Awake() {
        CancelEverything();
    }

    public void CancelEverything() {
        focusing.Stop();
        rewinding.Stop();
        freeze.Stop();
        focusing.Clear();
        rewinding.Clear();
        freeze.Clear();
    }

    public void FocusAnimation() {
        CancelEverything();
        focusing.Play();
    }

    public void RewindAnimation() {
        CancelEverything();
        rewinding.Play();
    }

    public void FreezeAnimation() {
        CancelEverything();
        freeze.Play();
    }
}
