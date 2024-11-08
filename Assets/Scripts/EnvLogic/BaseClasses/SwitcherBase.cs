using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherBase : MonoBehaviour
{
    [SerializeField] protected GameObject connectedObj;
    [SerializeField] protected GameObject oneTimeTrigger;
    protected bool isTriggered;
    private bool oneTimeSwitch;

    private void Awake() { isTriggered = false; }

    private void Trigger(GameObject _obj) {
        if (_obj.TryGetComponent<IPluggedTo>(out IPluggedTo interactable))
            interactable.Trigger();
    }

    protected virtual void Switch()
    {
        Trigger(connectedObj);

        if (!oneTimeSwitch)
        {
            oneTimeSwitch = true;
            Trigger(oneTimeTrigger);
        }
    }
}
