using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IPluggedTo
{
    [SerializeField] private Animator animator;
    private Collider doorCollider;
    private bool isOpen;

    private void Start()
    {
        doorCollider = GetComponent<Collider>();
        isOpen = false;
    }

    public void Trigger()
    {
        isOpen = !isOpen;
        doorCollider.enabled = !isOpen;
        animator.SetBool("isOpen", isOpen);
    }
}
