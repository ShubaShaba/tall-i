using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
    Base class for all pickable objects
*/
public class CarriableBase : MonoBehaviour, ICarriable
{
    [SerializeField] protected int keyID;
    private ICarrier carrier;
    protected Rigidbody rb;

    private void Awake() { rb = GetComponent<Rigidbody>(); }

    private void TogglePhysics(bool enable)
    {
        rb.isKinematic = !enable;
        rb.detectCollisions = enable;
        rb.freezeRotation = !enable;
    }

    public void Pickup(ICarrier parent)
    {
        if (carrier != null)
            carrier.Eject();
        
        OnPickup();
        if (!CanPick()) return;

        carrier = parent;
        carrier.AddCarriable(this);
        transform.parent = carrier.GetMountingPointTransform();
        transform.position = carrier.GetMountingPointTransform().position;
        transform.localRotation = Quaternion.identity;
        TogglePhysics(false);
    }

    public void Throw(Vector3 direction, bool ignoreCollision)
    {
        if (carrier == null) return;
        Collider[] hitColliders = Physics.OverlapBox(rb.position, transform.localScale / 2, transform.rotation);
        for (int i = 0; i < hitColliders.Length; i++)
            if (hitColliders[i].isTrigger == false && !ignoreCollision) return;

        carrier.RemoveCarriable(this);
        carrier = null;
        transform.parent = null;
        TogglePhysics(true);
        rb.velocity = direction;
    }

    public virtual bool CanPick() { return true; }

    protected virtual void OnPickup() { }

    public virtual int GetKeyID() { return keyID; }

    public bool IsPicked() { return carrier != null; }
}