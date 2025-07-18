using Obvious.Soap;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private Vector3Variable _playerPosition;

    void Update()
    {
        var direction = (_playerPosition.Value - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;
    }
}
