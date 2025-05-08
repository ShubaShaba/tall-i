using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherBase : MonoBehaviour
{
    [SerializeField] protected GameObject connectedObj;
    [SerializeField] private bool oneTimeSwitch = false;
    private bool isTriggered;

    private void Awake() { isTriggered = false; }

    private void Trigger(GameObject _obj) {
        isTriggered = true;
        if (_obj.TryGetComponent<IPluggedTo>(out IPluggedTo interactable))
            interactable.Trigger();
    }

    protected void Switch()
    {
        if (oneTimeSwitch && isTriggered) return;
        Trigger(connectedObj);
    }
}
