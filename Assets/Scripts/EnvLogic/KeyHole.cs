using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHole : SwitcherBase, ICarrier, IInteractable
{
    [SerializeField] private int keyID = 0;
    [SerializeField] private Transform mountingPoint;
    [SerializeField] private Transform unMountPoint;
    private ICarriable carriable;

    public void AddCarriable(ICarriable obj) { carriable = obj; }

    public Transform GetMountingPointTransform() { return mountingPoint; }

    public void Interact() { if (isInjected()) { Switch(); } }

    public void RemoveCarriable(ICarriable obj) { carriable = null; }

    private bool isInjected() { return carriable != null; }
    void Start() { carriable = null; }

    void OnTriggerEnter(Collider insertion)
    {
        if (insertion.TryGetComponent<ICarrier>(out ICarrier obj) && 
            obj.GetCarriable() != null && obj.GetCarriable().GetKeyID() == keyID)
        {
            obj.GetCarriable().Pickup(this);
        }
    }

    public void Eject() { carriable.Throw(transform.forward * -2, true); }

    public ICarriable GetCarriable() { return carriable; }
}