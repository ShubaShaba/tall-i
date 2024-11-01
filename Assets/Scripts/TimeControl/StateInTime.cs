using UnityEngine;

public struct StateInTime
{
    public Vector3 position { get; }
    public Quaternion rotation { get; }

    public StateInTime(Vector3 _position, Quaternion _rotation)
    {
        position = _position;
        rotation = _rotation;
    }

    public override string ToString() => $"({position}, {rotation})";
}