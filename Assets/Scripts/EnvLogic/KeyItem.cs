using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject keyHoleObj;
    private KeyHole keyHole;

    void Start()
    {
        keyHole = keyHoleObj.GetComponent<KeyHole>();
    }

    public void Interact()
    {
        keyHole.GrantClearance();
        Destroy(gameObject);
    }
}
