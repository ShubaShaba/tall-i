using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICarrier
{
    public void AddCarriable(ICarriable obj);
    public void RemoveCarriable(ICarriable obj);
    public Transform GetMountingPointTransform();
    public void Eject();
    public ICarriable GetCarriable();
}
