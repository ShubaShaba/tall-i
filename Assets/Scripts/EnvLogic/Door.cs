using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IPluggedTo
{
    public void Trigger()
    {
        Debug.Log("Door is being interacted with");
    }
}
