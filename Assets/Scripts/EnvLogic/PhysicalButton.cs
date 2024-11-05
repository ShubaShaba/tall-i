using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalButton : SwitcherBase
{
    private Collider colliderRef;

    void Start()
    {
        colliderRef = GetComponent<Collider>();
        colliderRef.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other) { Switch(); }

    private void OnTriggerExit(Collider other) { Switch(); }
}
