using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Base class for all pickable objects
*/
public class CarriableBase : MonoBehaviour, ICarriable
{
    protected Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(rb);
    }

    private void TogglePhysics(bool enable)
    {
        rb.isKinematic = !enable;
        rb.detectCollisions = enable;
        rb.freezeRotation = !enable;
    }

    public void Pickup(Transform parent)
    {
        if (!CanPick()) return;

        transform.parent = parent;
        transform.position = parent.position;
        transform.localRotation = Quaternion.identity;
        TogglePhysics(false);
    }

    public void Throw(float magnitude)
    {
        transform.parent = null;
        TogglePhysics(true);
        rb.velocity = transform.forward * magnitude;
    }

    public virtual bool CanPick()
    {
        return true;
    }
}