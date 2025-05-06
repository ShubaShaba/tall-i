using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("TEST");
        Destroy(gameObject);
    }
}
