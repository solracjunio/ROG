using Obvious.Soap;
using UnityEngine;

public class FloatVariablePickup : Pickup
{
    [SerializeField] private FloatVariable _floatVariable;
    [SerializeField] private FloatReference _value;

    protected override void OnTriggerEnter(Collider other)
    {
        _floatVariable.Add(_value);
        base.OnTriggerEnter(other);
    }
}
