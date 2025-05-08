using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHole : SwitcherBase, IInteractable
{
    private bool hasClearance;

    void Start()
    {
        hasClearance = false;
    }

    public virtual void Interact()
    {
        if (hasClearance) Switch();
    }

    public void GrantClearance()
    {
        hasClearance = true;
    }
}