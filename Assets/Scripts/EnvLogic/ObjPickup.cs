using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Base class for all pickable objects
*/
public class ObjPickup : MonoBehaviour, ICarriable
{
    [SerializeField] private Transform cameraTrans;
    private bool pickedup;
    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void TogglePhysics(bool enable) {
        rb.isKinematic = !enable;
        rb.detectCollisions = enable;
        rb.freezeRotation = !enable;
    }

    public void Pickup(Transform parent)
    {
        transform.parent = parent;
        transform.position = parent.position;
        transform.localRotation = Quaternion.identity;
        TogglePhysics(false);
    }

    public void Throw(float magnitude)
    {
        transform.parent = null;
        TogglePhysics(true);
        rb.velocity = cameraTrans.forward * magnitude;
    }

    public bool canPick()
    {
        return true;
    }
}