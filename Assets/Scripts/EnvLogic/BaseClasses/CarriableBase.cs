using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Base class for all pickable objects
*/
public class CarriableBase : MonoBehaviour, ICarriable
{
    private ICarrier carrier;
    protected Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void TogglePhysics(bool enable)
    {
        rb.isKinematic = !enable;
        rb.detectCollisions = enable;
        rb.freezeRotation = !enable;
    }

    public void Pickup(ICarrier parent)
    {
        OnPickup();
        if (!CanPick()) return;

        carrier = parent;
        carrier.AddCarriable(this);
        transform.parent = carrier.GetMountingPointTransform();
        transform.position = carrier.GetMountingPointTransform().position;
        transform.localRotation = Quaternion.identity;
        TogglePhysics(false);
    }

    public void Throw(float magnitude)
    {
        if (carrier == null) return;

        carrier.RemoveCarriable(this);
        carrier = null;
        transform.parent = null;
        TogglePhysics(true);
        rb.velocity = transform.forward * magnitude;
    }

    public virtual bool CanPick()
    {
        return true;
    }

    protected virtual void OnPickup() { }

    public bool IsPicked()
    {
        return carrier != null;
    }
}