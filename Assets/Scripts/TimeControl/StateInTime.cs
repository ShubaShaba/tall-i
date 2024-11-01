using UnityEngine;

public struct StateInTime
{
    public Vector3 position { get; }
    public Quaternion rotation { get; }
    public Vector3 linearVelocity { get; }
    public Vector3 angularVelocity { get; }

    public StateInTime(Vector3 _position, Quaternion _rotation, Vector3 _linearVelocity, Vector3 _angularVelocity)
    {
        position = _position;
        rotation = _rotation;
        linearVelocity = _linearVelocity;
        angularVelocity = _angularVelocity;
    }

    public override string ToString() => $"({position}, {rotation}, {linearVelocity}, {angularVelocity})";
}