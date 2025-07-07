using Obvious.Soap;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private Vector3Variable _inputs;

    private void Update()
    {
        _inputs.Value = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }
}
