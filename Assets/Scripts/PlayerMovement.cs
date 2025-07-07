using Obvious.Soap;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector3Variable _inputs;
    [SerializeField] private float _speed = 5f;

    private void Update()
    {
        Vector3 movement = _speed * Time.deltaTime * _inputs.Value.normalized;
        transform.Translate(movement, Space.World);
    }
}