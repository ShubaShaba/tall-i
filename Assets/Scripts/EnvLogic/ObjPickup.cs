using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPickup : MonoBehaviour
{
    [SerializeField] private Transform cameraTrans;
    [SerializeField] private float throwAmount;
    private bool interactable, pickedup;
    private Rigidbody objRigidbody;

    void Start() {
        objRigidbody = GetComponent<Rigidbody>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
            interactable = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            interactable = false;
            if (pickedup)
            {
                transform.parent = null;
                objRigidbody.useGravity = true;
                pickedup = false;
            }
        }
    }
    void Update()
    {
        if (interactable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                transform.parent = cameraTrans;
                objRigidbody.useGravity = false;
                pickedup = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                transform.parent = null;
                objRigidbody.useGravity = true;
                pickedup = false;
            }
            if (pickedup == true)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    pickedup = false;
                    transform.parent = null;
                    objRigidbody.useGravity = true;
                    objRigidbody.velocity = cameraTrans.forward * throwAmount;
                }
            }
        }
    }
}