using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHole : SwitcherBase, ICarrier, IInteractable
{
    [SerializeField] private int keyID = 0;
    [SerializeField] private Transform mountingPoint;
    private ICarriable carriable;

    public void AddCarriable(ICarriable obj) { carriable = obj; }

    public Transform GetMountingPointTransform() { return mountingPoint; }

    public void Interact() { if (isInjected()) { Switch(); } }

    public void RemoveCarriable(ICarriable obj) { carriable = null; }

    private bool isInjected() { return carriable != null; }
    void Start() { carriable = null; }

    void OnTriggerEnter(Collider insertion)
    {
        if (insertion.TryGetComponent<ICarriable>(out ICarriable obj) && obj.GetKeyID() == keyID)
        {
            Debug.Log("AAAAAAAAA");
            obj.Pickup(this);
        }
    }

    void FixedUpdate()
    {
        // Debug.Log(isInjected());
    }
}