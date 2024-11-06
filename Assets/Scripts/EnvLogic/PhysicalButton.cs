using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalButton : SwitcherBase
{
    private Collider colliderRef;
    private int counter;

    void Start()
    {
        colliderRef = GetComponent<Collider>();
        colliderRef.isTrigger = true;
        counter = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (counter == 0) Switch();
        counter++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (counter == 1) Switch();
        counter--;
    }
}
