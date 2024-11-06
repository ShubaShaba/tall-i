using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICarriable
{
    public void Pickup(ICarrier parent);
    public void Throw(Vector3 direction, bool ignoreCollision);
    public bool CanPick();
    public bool IsPicked();
    public int GetKeyID();
}
