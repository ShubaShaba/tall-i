using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherBase : MonoBehaviour
{
    [SerializeField] protected GameObject connectedObj;
    protected bool isTriggered;

    private void Awake() { isTriggered = false; }

    protected virtual void Switch()
    {
        if (connectedObj.TryGetComponent<IPluggedTo>(out IPluggedTo interactable))
            interactable.Trigger();
    }
}
