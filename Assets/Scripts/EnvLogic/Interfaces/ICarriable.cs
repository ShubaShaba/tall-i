using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICarriable 
{
    public void Pickup(Transform parent);
    public void Throw(float magnitude);
    public bool CanPick();
}
