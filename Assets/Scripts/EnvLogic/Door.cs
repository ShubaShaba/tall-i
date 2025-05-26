using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IPluggedTo
{
    [SerializeField] private Animator animator;
    private Collider doorCollider;
    [SerializeField] private bool isOpen = false;

    private void Start()
    {
        doorCollider = GetComponent<Collider>();
        doorCollider.enabled = !isOpen;
        animator.SetBool("isOpen", isOpen);
    }

    public void Trigger()
    {
        isOpen = !isOpen;
        doorCollider.enabled = !isOpen;
        animator.SetBool("isOpen", isOpen);
    }
}
