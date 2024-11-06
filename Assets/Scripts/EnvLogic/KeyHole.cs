using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHole : SwitcherBase, ICarrier, IInteractable
{
    [SerializeField] protected int keyID = 0;
    [SerializeField] protected Transform mountingPoint;
    protected ICarriable carriable;

    public void AddCarriable(ICarriable obj) { carriable = obj; }

    public Transform GetMountingPointTransform() { return mountingPoint; }

    public virtual void Interact() { if (isInjected()) { Switch(); } }

    public void RemoveCarriable(ICarriable obj) { carriable = null; }

    protected bool isInjected() { return carriable != null; }
    void Start() { carriable = null; }

    void OnTriggerEnter(Collider insertion)
    {
        if (insertion.TryGetComponent<ICarrier>(out ICarrier obj) &&
            obj.GetCarriable() != null && obj.GetCarriable().GetKeyID() == keyID)
        {
            obj.GetCarriable().Pickup(this);
        }
    }

    public virtual void Eject()
    {
        if (isTriggered)
        {
            Switch();
            isTriggered = false;
        }
        carriable?.Throw(transform.forward * -2, true);
    }

    public ICarriable GetCarriable() { return carriable; }
}