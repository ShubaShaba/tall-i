using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : SwitcherBase, IInteractable
{
    [SerializeField] private float timeBeforeSwitchBack = 0.0f;
    private bool ignoreInteract = false;
    
    public void Interact() {
        if (ignoreInteract) return;
        Switch();
        if (timeBeforeSwitchBack > 0.0f) {
            ignoreInteract = true;
            Invoke(nameof(SwitchBack), timeBeforeSwitchBack);   
        }
    }

    private void SwitchBack() {
        ignoreInteract = false;
        Switch();
    } 
}
